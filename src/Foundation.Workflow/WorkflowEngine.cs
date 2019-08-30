using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    

    public class WorkflowEngine : IWorkflowHost
    {
        private readonly IWorkflowPersistence _repository;
        private readonly IWorkflowExecutor _executor;
        private readonly IWorkflowDefinitionRegistrar _registrar;
        public Task PublishActionEvent(string eventName, string eventKey, WorkflowActionEvent evt)
        {
            var workflowId = Guid.Parse(eventKey.Split('.')[0]);
            return PublishActionEvent(workflowId, evt);
        }

        public IWorkflowDefinitionRegistrar Registrar => _registrar;

        public WorkflowEngine(IWorkflowPersistence repository, IWorkflowExecutor executor)
        {
            _repository = repository;
            _executor = executor;
            _registrar = new WorkflowDefinitionRegistrar();
        }


        public async Task<Guid> StartWorkflow<TWorkflowData>(string name, TWorkflowData data, string reference = null)
        {
            var definition =  _registrar.GetWorkflowDefinition(name);
            var workflowId = Guid.NewGuid();
            var workflow = WorkflowInstance.Start(workflowId,definition);
            workflow.StartStep(definition.Steps.First().Id);
            await _repository.AddWorkflow(workflow);
            await _executor.ExecuteWorkflow(definition, workflowId);
            return workflowId;
        }

        public async Task PublishActionEvent(Guid workflowId, WorkflowActionEvent evt)
        {
            var workflow = await _repository.GetWorkflowInstance(workflowId);
            var definition =  _registrar.GetWorkflowDefinition(workflow.WorkflowDefinitionName, workflow.Version);
            
            await _executor.ExecuteWorkflowAction(definition, workflowId, evt);
        }
        
    }
}