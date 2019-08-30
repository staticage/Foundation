using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface ILifeCycleEventPublisher
    {
        Task Publish(LifeCycleEvent evt);
    }
}