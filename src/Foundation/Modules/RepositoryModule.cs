using Autofac;
using Foundation.CQRS.EventStores;

namespace Foundation.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventStoreEventPublisher>().As<IEventStorePublisher>().InstancePerLifetimeScope();
            
        }
    }
}
