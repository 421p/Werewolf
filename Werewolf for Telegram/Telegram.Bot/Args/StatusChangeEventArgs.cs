using System;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Args
{
    public class StatusChangeEventArgs : EventArgs
    {
        public Status Status { get; private set; }

        internal StatusChangeEventArgs(Status status)
        {
            Status = status;
        }
    }
}