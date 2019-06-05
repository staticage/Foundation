using System.Reflection;
using Autofac;
using Foundation.CQRS;
using Foundation.Messaging;
using Rebus.Handlers;

namespace Foundation.Core
{
    public static class AutofacExtensions 
    {
        public static ContainerBuilder RegisterCommandHandlers(
            this ContainerBuilder builder, Assembly commandHandlersAssembly)
        {
            builder.RegisterAssemblyTypes(commandHandlersAssembly)
                .Where(type => type.IsImplementsInterface(typeof(ICommandHandler<>))).AsImplementedInterfaces()
                .InstancePerDependency();
            
            return builder;
        }

        public static ContainerBuilder RegisterQueries(
            this ContainerBuilder builder, Assembly queriesAssembly)
        {
            builder.RegisterAssemblyTypes(queriesAssembly)
                .Where(type => type.IsImplementsInterface(typeof(IQueryHandler<,>))).AsImplementedInterfaces()
                .InstancePerDependency();
            return builder;
        }

        public static ContainerBuilder RegisterEventHandlers(
            this ContainerBuilder builder, Assembly eventHandlersAssembly)
        {
            builder.RegisterAssemblyTypes(eventHandlersAssembly)
                .Where(type => type.IsImplementsInterface(typeof(IEventHandler<>))).AsImplementedInterfaces()
                .InstancePerDependency();
            return builder;
        }

        public static ContainerBuilder RegisterAllHandlers(
            this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.IsImplementsInterface(typeof(IHandleMessages))).AsImplementedInterfaces()
                .InstancePerDependency();
            return builder;
        }
    }
}
