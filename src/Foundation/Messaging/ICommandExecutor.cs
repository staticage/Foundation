using System.Threading.Tasks;
using Autofac;
using Foundation.CQRS;

namespace Foundation.Messaging
{
    public interface ICommandExecutor
    {
        Task Execute(ICommand command);
    }

    internal class CommandExecutor : ICommandExecutor
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandExecutor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public Task Execute(ICommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = (dynamic)_lifetimeScope.Resolve(commandHandlerType);
            return handler.Handle((dynamic)command);
        }
    }
}
