using System;

using Parser.LogFile.Reader.Contracts;

namespace Parser.WPFClient.Implementations
{
    public class WpfCommandUtilizationUpdateStrategy : ICommandUtilizationUpdateStrategy, IOnUpdateContainer
    {
        public event EventHandler<UpdateEventArgs> OnUpdate;

        public void DisplayUpdate(string update)
        {
            this.OnUpdate?.Invoke(null, new UpdateEventArgs(update));
        }
    }
}
