using System;

namespace Parser.Common.Contracts
{
    public interface IGenericEventHandlerProvider<EventArgsType>
        where EventArgsType : EventArgs
    {
        void Subscribe(Action<object, EventArgsType> action);

        void Unsubscribe(Action<object, EventArgsType> action);

        void Raise();
    }
}
