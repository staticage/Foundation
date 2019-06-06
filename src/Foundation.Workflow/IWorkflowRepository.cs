using System;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowRepository
    {
        Task AddWorkflowDefinition(string workflowDefinitionId, WorkflowDefinition definition);
        Task<WorkflowDefinition> GetWorkflowDefinition(string name, int version);
        Task AddWorkflow(Workflow workflow);
        Task<Workflow> GetWorkflow(Guid workflowId);
    }
}