using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Foundation.DDD;
using Foundation.Messaging;
using Rebus.Bus;


namespace Foundation.CQRS.EventStores
{
    public interface IEventStorePublisher
    {
        void Publish(IDomainEvent @event);
        Task AcceptAsync();
    }

    public class DummyEventStoreEventPublisher : IEventStorePublisher
    {
        public void Publish(IDomainEvent @event)
        {
            
        }

        public Task AcceptAsync()
        {
            return Task.CompletedTask;
        }
    }

    class EventStoreEventPublisher : IEventStorePublisher
    {
        private readonly IServiceBus _bus;
        private List<IDomainEvent> _unpublishedEvents = new List<IDomainEvent>();
        
        public EventStoreEventPublisher(IServiceBus bus)
        {
            _bus = bus;
        }
        
        public void Publish(IDomainEvent @event)
        {
            _unpublishedEvents.Add(@event);
        }

        public async Task AcceptAsync()
        {
            foreach (var @event in _unpublishedEvents)
            {
                await _bus.Publish(@event);
            }
        }
        
      
    }
}