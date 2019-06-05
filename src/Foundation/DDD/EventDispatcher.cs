using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Core;

namespace Foundation.DDD
{
    public class EventDispatcher<TEventInterface> where TEventInterface : IAggregateRootEvent
    {
        readonly List<KeyValuePair<Type, Action<object>>> _handlers = new List<KeyValuePair<Type, Action<object>>>();

        public IEventHandlerRegistrar<TEventInterface> RegisterHandlers() { return new EventHandlerRegistrar(this); }

        class EventHandlerRegistrar : IEventHandlerRegistrar<TEventInterface>
        {
            readonly EventDispatcher<TEventInterface> _owner;
            internal EventHandlerRegistrar(EventDispatcher<TEventInterface> owner) => _owner = owner;
            IEventHandlerRegistrar<TEventInterface> IEventHandlerRegistrar<TEventInterface>.For<TEvent>(Action<TEvent> handler)
            {
                _owner._handlers.Add(new KeyValuePair<Type, Action<object>>(typeof(TEvent), e => handler((TEvent)e)));
                return this;
            }
        }

        public IEnumerable<Action<object>> GetHandlers(Type eventType)
        {
            return _handlers.Where(x => eventType.IsImplementsInterface(x.Key))
                .Select(x => x.Value).ToList();
        }

        public void Dispatch(IAggregateRootEvent aggregateRootEvent)
        {
            var handlers = GetHandlers(aggregateRootEvent.GetType());
            handlers.ForEach(handler => handler.Invoke(aggregateRootEvent));
        }
    }
}