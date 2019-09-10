using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation.CQRS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Messaging
{
    public class  SynchronousBus : IServiceBus
    {
        private readonly IServiceProvider _lifetimeScope;

        public SynchronousBus(IServiceProvider lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task Send(ICommand command)
        {
            using (var scope = _lifetimeScope.CreateScope())
            {
                //                using (var transaction = new TransactionScope(
                //                    TransactionScopeOption.RequiresNew,
                //                    new TransactionOptions{IsolationLevel = IsolationLevel.ReadCommitted},
                //                    TransactionScopeAsyncFlowOption.Enabled))
                //                {
                try
                {
                    //                        var eventPublisher = scope.Resolve<IEventStorePublisher>();
                    //                        Transaction.Current.TransactionCompleted +=  (sender,e) =>
                    //                        {
                    //                            eventPublisher.AcceptAsync().Wait();
                    //                        };

                    

                    await scope.ServiceProvider.GetService<ICommandExecutor>().Execute(command);
                    
                    foreach (var repository in scope.ServiceProvider.GetServices<IRepository>())
                    {
//                        using (var transaction = await repository.GetTransactionAsync())
//                        {
//                            try
//                            {
                        await repository.SaveChangesAsync();   
//                                transaction.Commit();
//                            }
//                            catch
//                            {
//                                transaction.Rollback();
//                                throw;
//                            }
//                        }
                    }
                    
                    await Task.WhenAll(scope.ServiceProvider.GetServices<DbContext>().Select(x => x.SaveChangesAsync()));
                    
                    //                        transaction.Complete();
                }
                catch (Exception e)
                {
                    throw;
                }
                //                }

            }

        }

        public Task Publish(IEvent @event)
        {
            var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
            var handlersType = typeof(IEnumerable<>).MakeGenericType(eventHandlerType);
            dynamic handlers = _lifetimeScope.GetService(handlersType);
            foreach (var handler in handlers)
            {
                handler.Handle((dynamic)@event);
            }
            return Task.CompletedTask;
        }

        public Task<TResult> Get<TResult>(IQuery<TResult> query) where TResult : class
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _lifetimeScope.GetService(handlerType);
            return handler.Handle((dynamic)query);
        }

        public Task Defer(ICommand command, TimeSpan delay)
        {
            throw new NotImplementedException($"{nameof(SynchronousBus)} 不支持Defer操作");
        }
    }
}