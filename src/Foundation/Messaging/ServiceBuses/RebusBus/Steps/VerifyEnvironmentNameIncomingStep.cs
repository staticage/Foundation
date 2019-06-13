using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation.Core;
using Foundation.CQRS;
using Foundation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Messages;
using Rebus.Pipeline;
using Rebus.Transport;

namespace Foundation.Messaging.ServiceBuses.RebusBus.Steps
{
    [StepDocumentation("Verify EnvironmentName when incoming.")]
    class VerifyEnvironmentNameIncomingStep : IIncomingStep
    {
        public async Task Process(IncomingStepContext context, Func<Task> next)
        {
            var message = context.Load<Message>();
            var environment = message.Headers[HeaderKeys.EnvironmentName];
            if (environment.IsNullOrWhiteSpace())
            {
                throw new InvalidActionException("Missing environment name.");
            }

            if (!environment.Equals(Environments.EnvironmentName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidActionException($"EnvironmentName: {environment} does not match the local env ${Environments.EnvironmentName}");
            }

            await next();
        }
    }
    
    [StepDocumentation("Calling DbContext.SaveChanges.")]
    class EntityFrameworkSaveChangesStep : IIncomingStep
    {
        public async Task Process(IncomingStepContext context, Func<Task> next)
        {
            var scope = context.Load<ITransactionContext>()?.Items["current-autofac-lifetime-scope"] as IServiceProvider;
            foreach (var dbContext in scope.GetServices<DbContext>())
            {
                await dbContext.SaveChangesAsync();
            }
            foreach (var repository in scope.GetServices<IRepository>())
            {
                await repository.SaveChangesAsync();
            }
            //await scope.Resolve<IEventStorePublisher>().AcceptAsync();
            await next();
        }
    }
}