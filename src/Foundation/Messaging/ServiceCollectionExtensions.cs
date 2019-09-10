using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceBus(this IServiceCollection services)
        {
            services.AddScoped<IServiceBus, SynchronousBus>();
        }
    }
}