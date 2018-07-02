using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using LanguageFileConverter;
using Microsoft.Win32;
using Storage;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Werewolf_Control.Handler;
using Werewolf_Control.Models;

namespace Werewolf_Control.Helpers
{
    internal static class Bot
    {
        internal static string TelegramApiKey;
        public static readonly HashSet<Node> Nodes = new HashSet<Node>();
        public static Client Api;

        public static User Me;
        public static DateTime StartTime = DateTime.UtcNow;
        public static bool Running = true;
        public static long CommandsReceived = 0;
        public static long MessagesProcessed = 0;
        public static long MessagesReceived = 0;
        public static long TotalPlayers = 0;
        public static long TotalGames = 0;
        public static readonly Random R = new Random();
        public static XDocument English;
        public static int MessagesSent;
        public static string CurrentStatus = "";

        internal static string RootDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static readonly string LogDirectory = Path.Combine(RootDirectory, "../Logs/");

        internal delegate void ChatCommandMethod(Update u, string[] args);

        internal static readonly List<Command> Commands = new List<Command>();
        internal static string LanguageDirectory => Path.GetFullPath(Path.Combine(RootDirectory, @"../../Languages"));
        internal static string TempLanguageDirectory =>
            Path.GetFullPath(Path.Combine(RootDirectory, @"../../TempLanguageFiles"));

        public static void Initialize(string updateid = null)
        {
            //get api token from registry
            var key =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                    .OpenSubKey("SOFTWARE\\Werewolf");

            TelegramApiKey = key.GetValue("ProductionAPI").ToString();

            Api = new Client(TelegramApiKey, LogDirectory);

            English = LanguageConverter.Load(Path.Combine(LanguageDirectory, "English.yaml"));

            //load the commands list
            foreach (var m in typeof(Commands).GetMethods())
            {
                var c = new Command();
                foreach (var a in m.GetCustomAttributes(true))
                {
                    if (!(a is Attributes.Command ca))
                    {
                        continue;
                    }

                    c.Blockable = ca.Blockable;
                    c.DevOnly = ca.DevOnly;
                    c.GlobalAdminOnly = ca.GlobalAdminOnly;
                    c.GroupAdminOnly = ca.GroupAdminOnly;
                    c.Trigger = ca.Trigger;
                    c.Method = (ChatCommandMethod) Delegate.CreateDelegate(typeof(ChatCommandMethod), m);
                    c.InGroupOnly = ca.InGroupOnly;
                    Commands.Add(c);
                }
            }

            Api.InlineQueryReceived += UpdateHandler.InlineQueryReceived;
            Api.UpdateReceived += UpdateHandler.UpdateReceived;
            Api.CallbackQueryReceived += UpdateHandler.CallbackReceived;
            Api.ReceiveError += ApiOnReceiveError;
            //Api.OnReceiveGeneralError += ApiOnOnReceiveGeneralError;
            Api.StatusChanged += ApiOnStatusChanged;
            //Api.UpdatesReceived += ApiOnUpdatesReceived;
            Me = Api.GetMeAsync().Result;
            //Api.OnMessage += ApiOnOnMessage;
            Console.Title += " " + Me.Username;
            if (!string.IsNullOrEmpty(updateid))
            {
                Api.SendTextMessageAsync(updateid, "Control updated\n" + Program.GetVersion());
            }

            StartTime = DateTime.UtcNow;

            //now we can start receiving
            Api.StartReceiving();
        }

        //private static void ApiOnOnReceiveGeneralError(object sender, ReceiveGeneralErrorEventArgs receiveGeneralErrorEventArgs)
        //{
        //    if (!Api.IsReceiving)
        //    {
        //        Api.StartReceiving();// cancellationToken: new CancellationTokenSource(1000).Token);
        //    }
        //    var e = receiveGeneralErrorEventArgs.Exception;
        //    using (var sw = new StreamWriter(Path.Combine(RootDirectory, "../Logs/apireceiveerror.log"), true))
        //    {
        //        sw.WriteLine($"{DateTime.UtcNow} {e.Message} - {e.StackTrace}\n{e.Source}");
        //    }
        //}

        internal static void ReplyToCallback(CallbackQuery query, string text = null, bool edit = true,
                                             bool showAlert = false, InlineKeyboardMarkup replyMarkup = null,
                                             ParseMode parsemode = ParseMode.Default)
        {
            //first answer the callback
            Api.AnswerCallbackQueryAsync(query.Id, edit ? null : text, showAlert);
            //edit the original message
            if (edit)
            {
                Edit(query, text, replyMarkup, parsemode);
            }
        }

        internal static Task<Message> Edit(CallbackQuery query, string text, InlineKeyboardMarkup replyMarkup = null,
                                           ParseMode parsemode = ParseMode.Default) => Edit(query.Message.Chat.Id,
            query.Message.MessageId, text, replyMarkup, parsemode);

        internal static Task<Message> Edit(long id, int msgId, string text, InlineKeyboardMarkup replyMarkup = null,
                                           ParseMode parsemode = ParseMode.Default)
        {
            MessagesSent++;
            return Api.EditMessageTextAsync(id, msgId, text, parsemode, replyMarkup: replyMarkup);
        }

        private static void ApiOnStatusChanged(object sender, StatusChangeEventArgs statusChangeEventArgs)
        {
            using (var db = new WWContext())
            {
                var id = 1;

                var b = db.BotStatus.Find(id);
                b.Status = statusChangeEventArgs.Status.ToString();
                CurrentStatus = b.Status;
                db.SaveChanges();
            }
        }


        private static void ApiOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            if (!Api.IsReceiving)
            {
                Api.StartReceiving();
            }

            var e = receiveErrorEventArgs.ApiRequestException;
            using (var sw = new StreamWriter(Path.Combine(RootDirectory, "../Logs/apireceiveerror.log"), true))
            {
                sw.WriteLine($"{DateTime.UtcNow} {e.ErrorCode} - {e.Message}\n{e.Source}");
            }
        }

        private static void Reboot()
        {
            Running = false;
            Program.Running = false;
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(4);
        }

        //TODO this needs to be an event
        public static void NodeConnected(Node n) { }

        //TODO this needs to be an event as well
        public static void Disconnect(this Node n, bool notify = true)
        {
            if (notify && n.Games.Count > 2)
            {
                foreach (var g in n.Games)
                {
                    Send(UpdateHandler.GetLocaleString("NodeShutsDown", g.Language), g.GroupId);
                }
            }

            Nodes.Remove(n);
        }

        /// <summary>
        ///     Gets the node with the least number of current games
        /// </summary>
        /// <returns>Best node, or null if no nodes</returns>
        public static Node GetBestAvailableNode()
        {
            //make sure we remove bad nodes first
            foreach (var n in Nodes.Where(x => x.TcpClient.Connected == false).ToList())
            {
                Nodes.Remove(n);
            }

            return Nodes.Where(x => x.ShuttingDown == false && x.CurrentGames < Settings.MaxGamesPerNode)
                .OrderBy(x => x.CurrentGames).FirstOrDefault(); //if this is null, there are no nodes
        }


        internal static Task<Message> Send(string message, long id, bool clearKeyboard = false,
                                           InlineKeyboardMarkup customMenu = null, ParseMode parseMode = ParseMode.Html)
        {
            MessagesSent++;
            //message = message.Replace("`",@"\`");
            if (clearKeyboard)
            {
                //var menu = new ReplyKeyboardRemove() { RemoveKeyboard = true };
                return Api.SendTextMessageAsync(id, message, replyMarkup: customMenu, disableWebPagePreview: true,
                    parseMode: parseMode);
            }

            if (customMenu != null)
            {
                return Api.SendTextMessageAsync(id, message, replyMarkup: customMenu, disableWebPagePreview: true,
                    parseMode: parseMode);
            }

            return Api.SendTextMessageAsync(id, message, true, parseMode: parseMode);
        }

        internal static GameInfo GetGroupNodeAndGame(long id)
        {
            var node = (Nodes.ToList().FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                            .FirstOrDefault(x => x.GroupId == id) ?? Nodes.ToList()
                            .FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                            .FirstOrDefault(x => x.GroupId == id)) ?? Nodes.ToList()
                           .FirstOrDefault(n => n.Games.Any(g => g.GroupId == id))?.Games
                           .FirstOrDefault(x => x.GroupId == id);
            return node;
        }
    }
}