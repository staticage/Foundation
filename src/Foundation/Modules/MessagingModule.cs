using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Foundation.Core;
using Foundation.CQRS;
using Foundation.Messaging;
using Foundation.Messaging.ServiceBuses;
using Foundation.Messaging.ServiceBuses.RebusBus;
using Microsoft.Extensions.Configuration;
using Rebus.Bus;
using Rebus.Bus.Advanced;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Retry.Simple;

namespace Foundation.Modules
{
    public class MessagingModule : Module
    {
        public const string InternalServiceBusKey = "InternalServiceBus";
        public const string ExternalServiceBusKey = "ExternalServiceBus";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SynchronousBus>().As<IServiceBus, SynchronousBus>().InstancePerLifetimeScope();
            builder.RegisterType<RebusServiceBus>().As<IServiceBus, RebusServiceBus>().InstancePerLifetimeScope();
            builder.RegisterType<ServiceBus>().As<IServiceBus>().InstancePerLifetimeScope();

            builder.RegisterType<CommandExecutor>().As<ICommandExecutor>().InstancePerLifetimeScope();
        }
    }

    public static class AutofacServiceBusExtensions
    {
        public static void RegisterDefaultRebus(this ContainerBuilder builder, string queueName,Func<RebusConfigurer, 
            IComponentContext, RebusConfigurer> customConfigure = null)
        {
            builder.RegisterRebus((cfg, context) =>
            {
                var rebusConfigurer = cfg
                    .Logging(l => l.Serilog())
                    .Transport(t =>
                    {
                        t.UseRabbitMq($"amqp://{context.Resolve<IConfiguration>()["EventBusConnection"]}", queueName);
                    })
                    .Options(opt =>
                    {
                        opt.SimpleRetryStrategy(errorQueueAddress:$"{queueName}.error",maxDeliveryAttempts:1);
                        opt.PrepareOutgoingMessageHeader();
                        opt.VerifyIncomingMessageHeader();
                        opt.EnableEntityFramework();
                        
                    });
                
                if (customConfigure != null)
                {
                    rebusConfigurer = customConfigure(cfg, context);
                }

                return rebusConfigurer;
            });

            builder.RegisterBuildCallback(async container =>
            {
                var rebus = container.Resolve<IBus>();
                var messageTypes = new List<Type>();
                foreach (var registration in container.ComponentRegistry.Registrations)
                {
                    messageTypes.AddRange(registration.Services
                        .OfType<IServiceWithType>()
                        .Where(x => x.ServiceType.IsGenericType && x.ServiceType.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                        .Select(x => x.ServiceType.GetGenericArguments()[0])
                        .Where(x=> x.IsImplementsInterface(typeof(IExternalMessage)))

                    );
                }
                foreach (var messageType in messageTypes.Distinct().ToList())
                {
                    await rebus.Subscribe(messageType);
                }
            });
        }

//        public static void WrapCommandHandlersToConsumer(this ContainerBuilder builder)
//        {
//            builder.RegisterCallback(registry =>
//            {
//                foreach (var registration in registry.Registrations)
//                {
//                    var serviceWithTypes = registration.Services.OfType<IServiceWithType>().ToList();
//                    
//                    foreach (var handlerType in serviceWithTypes
//                        .Where(x => x.ServiceType.IsImplementsInterface(typeof(ICommandHandler<>)))
//                        .Select(x=> x.ServiceType))
//                    {
//                        var commandType = handlerType.GenericTypeArguments[0];
//                        
//                        var rb = RegistrationBuilder.ForType(typeof(CommandHandlerWrapper<>).MakeGenericType(commandType))
//                            .As(typeof(IConsumer<>).MakeGenericType(commandType))
//                            .InstancePerLifetimeScope();
//                        
//                        registry.Register(rb.CreateRegistration());
//                    }
//                }
//            });
//        }
    }

//    public class CommandHandlerWrapper<TCommand> : IConsumer<TCommand> where TCommand : class, ICommand
//    {
//        private readonly ICommandHandler<TCommand> _handler;
//
//        public CommandHandlerWrapper(ICommandHandler<TCommand> handler)
//        {
//            _handler = handler;
//        }
//        
//        public Task Consume(ConsumeContext<TCommand> context)
//        {
//            return _handler.Handle(context.Message);
//        }
//    }
}