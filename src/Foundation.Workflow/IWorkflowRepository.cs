using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowRepository
    {
        Task AddWorkflow(Workflow workflow);
        Task<Workflow> GetWorkflow(Guid workflowId);
    }

    public interface IWorkflowDefinitionRegistrar
    {
        Task RegisterWorkflowDefinition(WorkflowDefinition definition);
        Task<WorkflowDefinition> GetWorkflowDefinition(string name, int version);
    }

    public class WorkflowDefinitionRegistrar : IWorkflowDefinitionRegistrar
    {
        IDictionary<string, List<WorkflowDefinition>> _definitions = new Dictionary<string, List<WorkflowDefinition>>();
        
        public Task RegisterWorkflowDefinition( WorkflowDefinition definition)
        {
            if (!_definitions.ContainsKey(definition.Id))
            {
                _definitions.Add(definition.Id, new List<WorkflowDefinition>());
            }

            _definitions[definition.Id].Add(definition);
            return Task.CompletedTask;
        }

        public Task<WorkflowDefinition> GetWorkflowDefinition(string workflowDefinitionId, int version)
        {
            var definitions = _definitions[workflowDefinitionId];
            var definition = version > 0 ? definitions[version - 1] : definitions.Last();
            return Task.FromResult(definition);
        }
    }
}