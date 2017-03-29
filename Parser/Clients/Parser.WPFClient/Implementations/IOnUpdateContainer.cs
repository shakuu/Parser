using System;

namespace Parser.WPFClient.Implementations
{
    public interface IOnUpdateContainer
    {
        event EventHandler<UpdateEventArgs> OnUpdate;
    }
}
