using System.Threading.Tasks;


namespace Foundation.CQRS
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }

    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query);
    }

    public interface IEventHandler<in TEvent> 
    {   
    }
}