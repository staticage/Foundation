using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public class WorkflowEngine : IWorkflowEngine
    {
        private readonly IWorkflowRepository _repository;
        private readonly IWorkflowExecutor _executor;

        public WorkflowEngine(IWorkflowRepository repository, IWorkflowExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }


        public async Task<Guid> StartWorkflow(string name, int version = 0)
        {
            var definition = await _repository.GetWorkflowDefinition(name, version);
            var workflowId = Guid.NewGuid();
            var workflow = Workflow.Start(workflowId,definition);
            workflow.StartStep(definition.Steps.First().Id);
            await _repository.AddWorkflow(workflow);
            await _executor.ExecuteWorkflow(definition, workflowId);
            return workflowId;
        }

        public async Task PublishActionEvent(Guid workflowId, WorkflowActionEvent evt)
        {
            var workflow = await _repository.GetWorkflow(workflowId);
            var definition = await _repository.GetWorkflowDefinition(workflow.WorkflowDefinitionId, workflow.Version);
            
            await _executor.ExecuteWorkflow(definition, workflowId, evt);
        }
        

        public Task RegisterWorkflowDefinition(string name, WorkflowDefinition definition)
        {
            return _repository.AddWorkflowDefinition(name, definition);
        }
    }
}