using System;
using System.Threading.Tasks;
using Foundation.CQRS;

namespace Foundation.Messaging
{
    public interface ICommandExecutor
    {
        Task Execute(ICommand command);
    }

    internal class CommandExecutor : ICommandExecutor
    {
        private readonly IServiceProvider _lifetimeScope;

        public CommandExecutor(IServiceProvider lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public Task Execute(ICommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = (dynamic)_lifetimeScope.GetService(commandHandlerType);
            return handler.Handle((dynamic)command);
        }
    }
}
