using System;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowExecutor
    {
        Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId);
        Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId, WorkflowActionEvent evt);
    }
}