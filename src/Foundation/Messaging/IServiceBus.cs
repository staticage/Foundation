using System;
using System.Threading.Tasks;
using Foundation.CQRS;

namespace Foundation.Messaging
{
    public interface IServiceBus
    {
        Task Send(ICommand command);
        Task Publish(IEvent @event);
        Task<TQueryResult> Get<TQueryResult>(IQuery<TQueryResult> query) where TQueryResult : class;
        Task Defer(ICommand command,TimeSpan delay);
    }
}
