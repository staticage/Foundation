using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation.Workflow;

namespace WorkflowCore.Interface
{
    public interface IPersistenceProvider
    {
        Task AddWorkflow(WorkflowInstance workflow);
        Task<WorkflowInstance> GetWorkflowInstance(Guid workflowInstanceId);
        Task<List<WorkflowInstance>> GetRunnableInstances(DateTime now);
        void EnsureStoreExists();
    }

    public interface IWorkflowDefinitionRegistrar
    {
        void RegisterWorkflowDefinition(WorkflowDefinition definition);
        WorkflowDefinition GetWorkflowDefinition(string workflowName, int? version = null);
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

        public WorkflowDefinition GetWorkflowDefinition(string workflowName, int? version )
        {
            var key = new Tuple<string,int>(workflowName, version ?? _definitions.Keys.Where(x=> x.Item1 == workflowName).Max(x=> x.Item2));
            return _definitions.ContainsKey(key) ? _definitions[key] : null;
        }
    }
    
   
}