using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    

    public class WorkflowEngine : IWorkflowEngine
    {
        private readonly IWorkflowRepository _repository;
        private readonly IWorkflowExecutor _executor;
        private readonly IWorkflowDefinitionRegistrar _registrar;
        public IWorkflowDefinitionRegistrar Registrar => _registrar;

        public WorkflowEngine(IWorkflowRepository repository, IWorkflowExecutor executor)
        {
            _repository = repository;
            _executor = executor;
            _registrar = new WorkflowDefinitionRegistrar();
        }


        public async Task<Guid> StartWorkflow(string name, int version = 0)
        {
            var definition = await _registrar.GetWorkflowDefinition(name, version);
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
            var definition = await _registrar.GetWorkflowDefinition(workflow.WorkflowDefinitionId, workflow.Version);
            
            await _executor.ExecuteWorkflow(definition, workflowId, evt);
        }
        
    }
}