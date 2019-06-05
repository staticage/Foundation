using System;

namespace Foundation.DDD
{
    public interface IEventHandlerRegistrar<in TEventInterface>
    {
        IEventHandlerRegistrar<TEventInterface> For<TEvent>(Action<TEvent> action) where TEvent : TEventInterface;
    }
}