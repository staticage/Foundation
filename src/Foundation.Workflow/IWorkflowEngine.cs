using System;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowEngine
    {
        Task<Guid> StartWorkflow(string name, int version = 0);
        Task PublishActionEvent(Guid workflowId, WorkflowActionEvent evt);
        IWorkflowDefinitionRegistrar Registrar { get; }
    }
}