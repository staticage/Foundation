using System.Linq;
using System.Reflection;
using Foundation.CQRS;
using Foundation.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Handlers;

namespace Foundation.Core
{
    public static class AutofacExtensions 
    {
//        public static IServiceCollection RegisterCommandHandlers(
//            this IServiceCollection builder, Assembly commandHandlersAssembly)
//        {
//            commandHandlersAssembly.GetTypes()
//                .Where(type => type.IsImplementsInterface(typeof(ICommandHandler<>))).ForEach(handlerType => builder.AddTransient());.AsImplementedInterfaces()
//                .InstancePerDependency();
//            
//            return builder;
//        }
//
//        public static IServiceCollection RegisterQueries(
//            this IServiceCollection builder, Assembly queriesAssembly)
//        {
//            builder.RegisterAssemblyTypes(queriesAssembly)
//                .Where(type => type.IsImplementsInterface(typeof(IQueryHandler<,>))).AsImplementedInterfaces()
//                .InstancePerDependency();
//            return builder;
//        }
//
//        public static IServiceCollection RegisterEventHandlers(
//            this IServiceCollection builder, Assembly eventHandlersAssembly)
//        {
//            builder.RegisterAssemblyTypes(eventHandlersAssembly)
//                .Where(type => type.IsImplementsInterface(typeof(IEventHandler<>))).AsImplementedInterfaces()
//                .InstancePerDependency();
//            return builder;
//        }
//
//        public static IServiceCollection RegisterAllHandlers(
//            this IServiceCollection builder, Assembly assembly)
//        {
//            builder.RegisterAssemblyTypes(assembly)
//                .Where(type => type.IsImplementsInterface(typeof(IHandleMessages))).AsImplementedInterfaces()
//                .InstancePerDependency();
//            return builder;
//        }
    }
}
