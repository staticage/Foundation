using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowPersistence
    {
        Task AddWorkflow(WorkflowInstance workflow);
        Task<WorkflowInstance> GetWorkflowInstance(Guid workflowId);
    }

    public interface IWorkflowDefinitionRegistrar
    {
        void RegisterWorkflowDefinition(WorkflowDefinition definition);
        WorkflowDefinition GetWorkflowDefinition(string workflowName, int version = 1);
    }

    public class WorkflowDefinitionRegistrar : IWorkflowDefinitionRegistrar
    {
        private readonly IDictionary<Tuple<string,int>, WorkflowDefinition> _definitions = new Dictionary<Tuple<string,int>, WorkflowDefinition>();

        public void RegisterWorkflowDefinition(WorkflowDefinition definition)
        {
            var key = new Tuple<string,int>(definition.Name, definition.Version);
            if (_definitions.ContainsKey(key))
            {
                throw new InvalidOperationException($"Duplicate workflow with name: {definition.Name} and version: {definition.Version}");
            }
            _definitions.Add(key, definition);
        }

        public WorkflowDefinition GetWorkflowDefinition(string workflowName, int version)
        {
            var key = new Tuple<string,int>(workflowName, version);
            return _definitions[key];
        }
    }
    
    public abstract class LifeCycleEvent
    {
        public DateTime EventTimeUtc { get; set; }

        public string WorkflowInstanceId { get; set; }

        public string WorkflowDefinitionId { get; set; }

        public int Version { get; set; }

        public string Reference { get; set; }
    }
}