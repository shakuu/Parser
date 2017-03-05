using System;

namespace Parser.Common.Contracts
{
    public interface IGenericSubscribeProvider<EventArgsType> where EventArgsType : EventArgs
    {
        void Subscribe(EventHandler<EventArgsType> action);
    }
}
