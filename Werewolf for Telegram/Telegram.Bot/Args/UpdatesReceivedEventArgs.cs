using System;

namespace Telegram.Bot.Args
{
    public class UpdatesReceivedEventArgs : EventArgs
    {
        public int UpdateCount { get; set; }
        public DateTime TimeReceived => DateTime.Now;

        internal UpdatesReceivedEventArgs(int count)
        {
            UpdateCount = count;
        }
    }
}