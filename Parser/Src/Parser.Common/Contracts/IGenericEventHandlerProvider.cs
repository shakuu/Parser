using System;

namespace Parser.Common.Contracts
{
    public interface IGenericEventHandlerProvider<EventArgsType> : IGenericSubscribeProvider<EventArgsType>
        where EventArgsType : EventArgs
    {
        void Unsubscribe(EventHandler<EventArgsType> action);

        void Raise(object sender, EventArgsType eventArgs);
    }
}
