using System;
using System.Collections.Generic;
using TeleSharp.TL;
using TLChatFull = TeleSharp.TL.Messages.TLChatFull;

namespace Werewolf_Control.Models
{
    public class ChannelInfo
    {
        private DateTime _dateCreated;
        public TLChannel Channel { get; set; }
        public TLChatFull ChatFull { get; set; }
        public List<TLUser> Users { get; set; } = new List<TLUser>();

        public DateTime DateCreated
        {
            get => _dateCreated;
            set => _dateCreated = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Channel.date)
                .ToLocalTime();
        }
    }
}