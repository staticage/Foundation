using System;
using System.Threading.Tasks;
using Foundation.CQRS;
using Rebus.Bus;

namespace Foundation.Messaging.ServiceBuses.RebusBus
{
    class RebusServiceBus : IServiceBus
    {
        private readonly IBus _bus;

        public RebusServiceBus(IBus bus)
        {
            _bus = bus;
        }

        public Task Send(ICommand command) => _bus.Send(command);
        public Task Publish(IEvent evt) => _bus.Publish(evt);

        public Task<TQueryResult> Get<TQueryResult>(IQuery<TQueryResult> query) where TQueryResult : class
        {
            throw new NotImplementedException();
//            var result = await _bus.SendRequest<TQueryResult>(query);
//            return result;
        }

        public Task Defer(ICommand command, TimeSpan delay) => _bus.Defer(delay, command);
    }
}