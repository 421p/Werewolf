using System;
using System.Collections.Generic;
using TeleSharp.TL;
using TLChatFull = TeleSharp.TL.Messages.TLChatFull;

namespace Werewolf_Control.Models
{
    public class ChannelInfo
    {
        public TLChannel Channel { get; set; }
        public TLChatFull ChatFull { get; set; }
        public List<TLUser> Users { get; set; } = new List<TLUser>();

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set
            {
                _dateCreated = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Channel.date)
                    .ToLocalTime();
            }
        }
    }
}