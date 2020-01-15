using System;
using System.Threading.Tasks;
using Foundation.Workflow;

namespace WorkflowCore.Interface
{
    public interface IWorkflowHost
    {
        Task<Guid> StartWorkflow<TWorkflowData>(string name, TWorkflowData data = default, string reference = null);
        Task PublishActionEvent(Guid workflowId, WorkflowActionEvent evt);
        Task PublishActionEvent(string eventName, string eventKey,WorkflowActionEvent evt );
        IWorkflowDefinitionRegistrar Registrar { get; }
        Task TerminateWorkflow(Guid workflowInstanceId);
    }
}