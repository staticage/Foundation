using System.Threading.Tasks;
using Rebus.Handlers;


namespace Foundation.CQRS
{
    public interface ICommandHandler<in TCommand> : IHandleMessages<TCommand>
        where TCommand : ICommand
    {
    }

    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query);
    }

    public interface IEventHandler<in TEvent> : IHandleMessages<TEvent>
    {   
    }
}