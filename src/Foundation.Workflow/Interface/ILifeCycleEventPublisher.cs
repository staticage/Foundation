using System.Threading.Tasks;
using WorkflowCore.Models.LifeCycleEvents;

namespace WorkflowCore.Interface
{
    public interface ILifeCycleEventPublisher
    {
        void Publish(LifeCycleEvent evt);
        Task Accept();
    }
}