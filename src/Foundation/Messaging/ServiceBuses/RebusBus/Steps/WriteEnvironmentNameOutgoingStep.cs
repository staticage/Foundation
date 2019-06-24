using System;
using System.Threading.Tasks;
using Foundation.Core;
using Rebus.Messages;
using Rebus.Pipeline;

namespace Foundation.Messaging.ServiceBuses.RebusBus.Steps
{
    [StepDocumentation("Set EnvironmentName when outgoing.")]
    internal class WriteEnvironmentNameOutgoingStep : IOutgoingStep
    {
        public async Task Process(OutgoingStepContext context, Func<Task> next)
        {
            var message = context.Load<Message>();
            message.Headers[HeaderKeys.EnvironmentName] = Environments.EnvironmentName;
            message.Headers[HeaderKeys.ApplicationName] = AppDomain.CurrentDomain.FriendlyName;
            message.Headers[HeaderKeys.MachineName] = Environment.MachineName;
            await next();
        }
    }
}