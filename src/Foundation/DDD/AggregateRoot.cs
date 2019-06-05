using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Core;
using Foundation.DDD.Exceptions;

namespace Foundation.DDD
{
    public abstract class AggregateRoot : Entity<Guid>, IAggregateRoot
    {
        readonly List<IAggregateRootEvent> _events;
        readonly EventDispatcher<IAggregateRootEvent> _dispatcher;
        protected AggregateRoot()
        {
            _events = new List<IAggregateRootEvent>();
            _dispatcher = new EventDispatcher<IAggregateRootEvent>();
        }
        
        protected IEventHandlerRegistrar<IAggregateRootEvent> RegisterEventHandlers()
        {
            return _dispatcher.RegisterHandlers();
        }

        private void ApplyEvent(IAggregateRootEvent aggregateRootEvent)
        {
            if (aggregateRootEvent.Version != Version + 1)
            {
                throw new IncorrectAggregateRootEventVersionException();
            }
            _dispatcher.Dispatch(aggregateRootEvent);
            Version = aggregateRootEvent.Version;
        }

        protected void RaiseEvent(IAggregateRootEvent aggregateRootEvent)
        {
            aggregateRootEvent.Version = Version + 1;
            aggregateRootEvent.EventTime = DateTime.Now;
            aggregateRootEvent.AggregateRootId = Id;
            _events.Add(aggregateRootEvent);
            ApplyEvent(aggregateRootEvent);
        }

        IEnumerable<IAggregateRootEvent> IAggregateRoot.GetChanges() => _events;
        public void LoadHistory(IEnumerable<IAggregateRootEvent> events)
        {
            if (Version != 0)
            {
                throw new IncorrectAggregateRootEventVersionException();
            }

            Id = events.First().AggregateRootId;
            events.ForEach(ApplyEvent);
        }

        public int Version { get; private set; }
        
        public void AcceptChanges()
        {
            _events.Clear();
        }
    }
}
