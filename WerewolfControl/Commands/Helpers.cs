using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using LanguageFileConverter;
using Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Werewolf_Control.Helpers;
using Werewolf_Control.Models;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
namespace Werewolf_Control
{
    public static partial class Commands
    {
        private static Player GetDBPlayer(int id, WWContext db) => db.Players.FirstOrDefault(x => x.TelegramId == id);

        private static void StartGame(bool chaos, Update update)
        {
            if (update.Message.Chat.Type == ChatType.Private)
            {
                //PM....  can't do that here
                Send(GetLocaleString("StartFromGroup", GetLanguage(update.Message.From.Id)), update.Message.Chat.Id);
                return;
            }

            Group grp;
            using (var db = new WWContext())
            {
                grp = db.Groups.FirstOrDefault(x => x.GroupId == update.Message.Chat.Id);
                if (grp == null)
                {
                    grp = MakeDefaultGroup(update.Message.Chat.Id, update.Message.Chat.Title, "StartGame");
                    db.Groups.Add(grp);
                }

                grp.Name = update.Message.Chat.Title;
                grp.UserName = update.Message.Chat.Username;
                grp.BotInGroup = true;
                if (grp.CreatedBy == "BAN")
                {
                    Bot.Api.LeaveChatAsync(grp.GroupId);
                    return;
                }

                if (!string.IsNullOrEmpty(update.Message.Chat.Username))
                {
                    grp.GroupLink = "https://telegram.me/" + update.Message.Chat.Username;
                }
                else if (!(grp.GroupLink?.Contains("joinchat") ?? true)
                ) //if they had a public link (username), but don't anymore, remove it
                {
                    grp.GroupLink = null;
                }

                //remove usernames & links from the other groups that have the same username in the db
                if (grp.UserName != null)
                {
                    var sameusername = db.Groups.Where(x =>
                        x.UserName == update.Message.Chat.Username && x.GroupId != update.Message.Chat.Id);
                    foreach (var g in sameusername)
                    {
                        g.UserName = null;
                        g.GroupLink = null;
                    }
                }

                db.SaveChanges();
            }

            //check nodes to see if player is in a game
            var node = GetPlayerNode(update.Message.From.Id);
            var game = GetGroupNodeAndGame(update.Message.Chat.Id);
            if (game != null || node != null)
            {
                //try grabbing the game again...
                if (game == null)
                {
                    game = node.Games.FirstOrDefault(x => x.Users.Contains(update.Message.From.Id));
                }

                if (game?.Users.Contains(update.Message.From.Id) ?? false)
                {
                    if (game.GroupId != update.Message.Chat.Id)
                    {
                        //player is already in a game, and alive
                        Send(
                            GetLocaleString("AlreadyInGame", grp?.Language ?? "English",
                                game.ChatGroup.ToBold()), update.Message.Chat.Id);
                        return;
                    }
                }

                //player is not in game, they need to join, if they can
                //game?.AddPlayer(update);
                game?.ShowJoinButton();
                if (game == null)
                {
                    Program.Log(
                        $"{update.Message.From.FirstName} tried to join a game on node {node?.ClientId}, but game object was null",
                        true);
                }

                return;
            }

            //no game found, start one
            node = Bot.GetBestAvailableNode();
            
            if (node != null)
            {
                node.StartGame(update, chaos);
                //notify waiting players
                using (var db = new WWContext())
                {
                    var notify = db.NotifyGame.Where(x => x.GroupId == update.Message.Chat.Id).ToList();

                    Console.WriteLine($"Sending notifications to {notify.Count} users.");
                    
                    var groupName = update.Message.Chat.Title.ToBold();

                    if (update.Message.Chat.Username != null)
                    {
                        groupName += $" @{update.Message.Chat.Username}";
                    }
                    else if (grp.GroupLink != null)
                    {
                        groupName = $"<a href=\"{grp.GroupLink}\">{update.Message.Chat.Title}</a>";
                    }

                    db.NotifyGame.RemoveRange(notify);
                    db.SaveChanges();

                    foreach (var n in notify)
                    {
                        if (n.UserId != update.Message.From.Id)
                        {
                            Console.WriteLine($"Sending game notification to user {n.UserId}");
                            Send(GetLocaleString("NotifyNewGame", grp.Language, groupName), n.UserId);
                        }

                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                Send(GetLocaleString("NoNodes", grp.Language), update.Message.Chat.Id);
            }
        }

        internal static Task<Message> Send(string message, long id, bool clearKeyboard = false,
                                           InlineKeyboardMarkup customMenu = null) =>
            Bot.Send(message, id, clearKeyboard, customMenu);


        private static string GetLocaleString(string key, string language, params object[] args)
        {
            try
            {
                var files = Directory.GetFiles(Bot.LanguageDirectory);
                XDocument doc;
                var file = files.First(x => Path.GetFileNameWithoutExtension(x) == language);
                {
                    doc = LanguageConverter.Load(file);
                }
                var strings = doc.Descendants("string").FirstOrDefault(x => x.Attribute("key").Value == key) ??
                              Bot.English.Descendants("string").FirstOrDefault(x => x.Attribute("key").Value == key);
                var values = strings.Descendants("value");
                var choice = Bot.R.Next(values.Count());
                var selected = values.ElementAt(choice);
                return string.Format(selected.Value.FormatHTML(), args).Replace("\\n", Environment.NewLine);
            }
            catch (Exception)
            {
                var strings = Bot.English.Descendants("string").FirstOrDefault(x => x.Attribute("key").Value == key);
                var values = strings.Descendants("value");
                var choice = Bot.R.Next(values.Count());
                var selected = values.ElementAt(choice);
                return string.Format(selected.Value.FormatHTML(), args).Replace("\\n", Environment.NewLine);
            }
        }

        internal static Group MakeDefaultGroup(long groupid, string name, string createdBy) => new Group
        {
            GroupId = groupid,
            Name = name,
            Language = "English",
            BotInGroup = true,
            ShowRoles = true,
            Mode = "Player",
            DayTime = Settings.TimeDay,
            LynchTime = Settings.TimeLynch,
            NightTime = Settings.TimeNight,
            AllowFool = true,
            AllowTanner = true,
            AllowCult = true,
            DisableFlee = false,
            MaxPlayers = 35,
            CreatedBy = createdBy,
            AllowExtend = false,
            MaxExtend = 60,
            EnableSecretLynch = false,
            ShowRolesEnd = "All",
            Flags = (long) GroupDefaults.LoadDefaults()
        };

        internal static void RequestPM(long groupid)
        {
            var button = new InlineKeyboardUrlButton("Start Me", "telegram.me/" + Bot.Me.Username);
            Send(GetLocaleString("StartMe", GetLanguage(groupid)), groupid,
                customMenu: new InlineKeyboardMarkup(new[] {button}));
        }

        private static Node GetPlayerNode(int id) =>
            (Bot.Nodes.ToList().FirstOrDefault(n => n.Games.Any(g => g.Users.Contains(id))) ??
             Bot.Nodes.ToList().FirstOrDefault(n => n.Games.Any(g => g.Users.Contains(id)))) ??
            Bot.Nodes.ToList().FirstOrDefault(n => n.Games.Any(g => g.Users.Contains(id)));

        private static GameInfo GetGroupNodeAndGame(long id) =>
            (Bot.Nodes.ToList().FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                 .FirstOrDefault(x => x.GroupId == id) ?? Bot.Nodes.ToList()
                 .FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                 .FirstOrDefault(x => x.GroupId == id)) ?? Bot.Nodes.ToList()
                .FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                .FirstOrDefault(x => x.GroupId == id);

        /// <summary>
        ///     Gets the language for the group, defaulting to English
        /// </summary>
        /// <param name="id">The ID of the group</param>
        /// <returns></returns>
        public static string GetLanguage(long id)
        {
            using (var db = new WWContext())
            {
                Player p = null;
                var grp = db.Groups.FirstOrDefault(x => x.GroupId == id);
                if (grp == null)
                {
                    p = db.Players.FirstOrDefault(x => x.TelegramId == id);
                }

                if (p != null && string.IsNullOrEmpty(p.Language))
                {
                    p.Language = "English";
                    db.SaveChanges();
                }

                return grp?.Language ?? p?.Language ?? "English";
            }
        }

        /// <summary>
        ///     Get language for a player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetLanguage(int id)
        {
            using (var db = new WWContext())
            {
                var p = db.Players.FirstOrDefault(x => x.TelegramId == id);
                if (string.IsNullOrEmpty(p?.Language) && p != null)
                {
                    p.Language = "English";
                    db.SaveChanges();
                }

                return p?.Language ?? "English";
            }
        }

        public static string GetLanguageName(string baseName)
        {
            var files = Directory.GetFiles(Bot.LanguageDirectory);
            XDocument doc;
            var file = files.First(x => Path.GetFileNameWithoutExtension(x) == baseName);
            {
                doc = LanguageConverter.Load(file);
            }
            var langNode = doc.Descendants("language").First();
            return $"{langNode.Attribute("base").Value}"; // - {langNode.Attribute("variant").Value}
        }

        internal static string GetAbout(Update update, string[] args)
        {
            var language = GetLanguage(update.Message.From.Id);
            var files = Directory.GetFiles(Bot.LanguageDirectory);
            XDocument doc;
            var file = files.First(x => Path.GetFileNameWithoutExtension(x) == language);
            {
                doc = LanguageConverter.Load(file);
            }
            var strings = doc.Descendants("string")
                .FirstOrDefault(x => x.Attribute("key").Value.ToLower() == args[0].ToLower());
            if (strings == null)
            {
                var efile = LanguageConverter.Load(Path.Combine(Bot.LanguageDirectory, "English.yaml"));
                strings =
                    efile.Descendants("string")
                        .FirstOrDefault(x => x.Attribute("key").Value.ToLower() == args[0].ToLower());
            }

            if (strings == null)
            {
                return null;
            }

            var values = strings.Descendants("value");
            var choice = Bot.R.Next(values.Count());
            var selected = values.ElementAt(choice);
            return string.Format(selected.Value.FormatHTML(), args).Replace("\\n", Environment.NewLine);
        }

        public static void KickChatMember(long chatid, int userid)
        {
            var status = Bot.Api.GetChatMemberAsync(chatid, userid).Result.Status;

            if (status == ChatMemberStatus.Administrator) //ignore admins
            {
                return;
            }

            //kick
            Bot.Api.KickChatMemberAsync(chatid, userid);
            //get their status
            status = Bot.Api.GetChatMemberAsync(chatid, userid).Result.Status;
            while (status == ChatMemberStatus.Member) //loop
            {
                //wait for database to report status is kicked.
                status = Bot.Api.GetChatMemberAsync(chatid, userid).Result.Status;
                Thread.Sleep(500);
            }
            //status is now kicked (as it should be)

            while (status != ChatMemberStatus.Left) //unban until status is left
            {
                Bot.Api.UnbanChatMemberAsync(chatid, userid);
                Thread.Sleep(500);
                status = Bot.Api.GetChatMemberAsync(chatid, userid).Result.Status;
            }

            //yay unbanned
        }

        public static int ComputeLevenshtein(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                {
                    return 0;
                }

                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (var i = 0; i <= n; d[i, 0] = i++)
            {
                ;
            }

            for (var j = 1; j <= m; d[0, j] = j++)
            {
                ;
            }

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = t[j - 1] == s[i - 1] ? 0 : 1;
                    var min1 = d[i - 1, j] + 1;
                    var min2 = d[i, j - 1] + 1;
                    var min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[n, m];
        }

        public static Group GetGroup(string str, WWContext db)
        {
            //try with id
            long id = 0;
            if (long.TryParse(str, out id))
            {
                return db.Groups.FirstOrDefault(x => x.GroupId == id);
            }

            //try with username
            if (str.StartsWith("@"))
            {
                return db.Groups.FirstOrDefault(x => x.UserName == str.Substring(1));
            }

            //hope str is a link, and compare the hash part
            var index = str.LastIndexOf("me/");
            if (index == -1)
            {
                return null;
            }

            var hash = str.Substring(index); //dummy variable becase LINQ to Entity doesn't like it.
            return db.Groups.FirstOrDefault(x => x.GroupLink.EndsWith(hash));
        }
    }
}