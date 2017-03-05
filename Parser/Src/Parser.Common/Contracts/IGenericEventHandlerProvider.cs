using System;

namespace Parser.Common.Contracts
{
    public interface IGenericEventHandlerProvider<EventArgsType>
        where EventArgsType : EventArgs
    {
        void Subscribe(EventHandler<EventArgsType> action);

        void Unsubscribe(EventHandler<EventArgsType> action);

        void Raise(object sender, EventArgsType eventArgs);
    }
}
