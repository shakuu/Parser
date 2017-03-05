using System;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class GenericEventHandlerProvider<EventArgsType> : IGenericEventHandlerProvider<EventArgsType>
        where EventArgsType : EventArgs
    {
        private event EventHandler<EventArgsType> onEvent;

        protected virtual event EventHandler<EventArgsType> OnEvent
        {
            add
            {
                this.onEvent += value;
            }

            remove
            {
                this.onEvent -= value;
            }
        }

        public virtual void Raise(EventArgsType eventArgs)
        {
            this.onEvent?.Invoke(this, eventArgs);
        }

        public virtual void Subscribe(EventHandler<EventArgsType> action)
        {
            this.OnEvent += action;
        }

        public virtual void Unsubscribe(EventHandler<EventArgsType> action)
        {
            this.OnEvent -= action;
        }
    }
}
