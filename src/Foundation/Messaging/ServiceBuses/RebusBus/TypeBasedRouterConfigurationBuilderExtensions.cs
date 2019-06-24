using Foundation.Core;
using Foundation.CQRS;
using TypeBasedRouterConfigurationBuilder = Rebus.Routing.TypeBased.TypeBasedRouterConfigurationExtensions.TypeBasedRouterConfigurationBuilder;

namespace Foundation.Messaging.ServiceBuses.RebusBus
{
    public static class TypeBasedRouterConfigurationBuilderExtensions
    {
        public static TypeBasedRouterConfigurationBuilder  MapAssemblyExternalCommandsOf<TCommand>(this TypeBasedRouterConfigurationBuilder builder, string destinationAddress) 
        {
            var assembly = typeof(TCommand).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsImplementsInterface(typeof(ICommand)) && type.IsImplementsInterface(typeof(IExternalMessage)))
                {
                    builder = builder.Map(type, destinationAddress);
                }
            }

            return builder;
        }
    }
}