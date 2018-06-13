using System;
using Telegram.Bot.Types;

namespace Werewolf_Control.Models
{
    internal class UserMessage
    {
        public UserMessage(Message m)
        {
            Time = m.Date;
            Command = m.Text;
        }

        public DateTime Time { get; set; }
        public string Command { get; set; }
        public bool Replied { get; set; }
    }
}