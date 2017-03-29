using System;

namespace Parser.WPFClient.Implementations
{
    public class UpdateEventArgs : EventArgs
    {
        public UpdateEventArgs(string updateMessage)
        {
            this.UpdateMessage = updateMessage;
        }

        public string UpdateMessage { get; private set; }
    }
}
