using System.Linq;
using Database;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Werewolf_Control.Helpers
{
    internal static class UpdateHelper
    {
        internal static int[] Devs =
        {
            268253251
        };

        internal static bool IsGroupAdmin(Update update) =>
            IsGroupAdmin(update.Message.From.Id, update.Message.Chat.Id);

        internal static bool IsGlobalAdmin(int id)
        {
            using (var db = new WWContext())
            {
                return db.Admins.Any(x => x.UserId == id);
            }
        }

        internal static bool IsGroupAdmin(int user, long group)
        {
            //fire off admin request
            try
            {
                //check all admins
                if (Bot.Api.GetChatAsync(group).Result.AllMembersAreAdministrators)
                {
                    return true;
                }

                var admin = Bot.Api.GetChatMemberAsync(group, user).Result;
                return admin.Status == ChatMemberStatus.Administrator || admin.Status == ChatMemberStatus.Creator;
            }
            catch
            {
                return false;
            }
        }
    }
}