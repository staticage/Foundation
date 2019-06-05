using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Workflow
{
    public class WorkflowDefinition
    {
        public string Description { get; set; }
        public List<StepDefinition> Steps { get; set; } = new List<StepDefinition>();

        public string GetNextStepId(string currentStepId)
        {
            var nextIndex = Steps.FindIndex(x => x.Id == currentStepId) + 1;
            return nextIndex < Steps.Count ? Steps[nextIndex].Id : null;
        }
    }

    public class StepDefinition
    {
        public string Id { get; set; }
        public Type BodyType { get; set; }
    }

    public class StepExecutionContext : IStepExecutionContext
    {
        public WorkflowDefinition WorkflowDefinition { get; set; }
        public Workflow Workflow { get; set; }
        public ExecutionPointer ExecutionPointer { get; set; }
    }

    public interface IStepExecutionContext
    {
        WorkflowDefinition WorkflowDefinition { get; }
        Workflow Workflow { get; }
        ExecutionPointer ExecutionPointer { get; }
    }

    public class ExecutionResult
    {
        public bool Proceed { get; set; }
        public string EventName { get; set; }
        public string EventKey { get; set; }
        public string NextStepId { get; set; }

        public static ExecutionResult Next(string nextStepId = null)
        {
            return new ExecutionResult {Proceed = true, NextStepId = nextStepId};
        }

        public static ExecutionResult WaitForEvent(string eventName, string eventKey)
        {
            return new ExecutionResult
            {
                EventName = eventName,
                EventKey = eventKey
            };
        }
    }


    public interface IStepBody
    {
        Task<ExecutionResult> RunAsync(IStepExecutionContext context);
    }

    public enum ExecutionPointerStatus
    {
        Running = 0,
        Completed
    }

    public enum WorkflowStatus
    {
        Completed = 5,
        Running
    }

    public class Workflow
    {
        public string Id { get; set; }
        private readonly WorkflowDefinition _definition;
        public List<ExecutionPointer> ExecutionPointers { get; set; }
        public WorkflowStatus Status { get; set; }

        public Workflow(string id,WorkflowDefinition definition)
        {
            Id = id;
            _definition = definition;
        }

        public ExecutionPointer StartStep(string stepDefinitionId)
        {
            if (_definition.Steps.All(x => x.Id != stepDefinitionId))
            {
                throw new InvalidOperationException($"Invalid stepId {stepDefinitionId}");
            }

            var executionPointer = new ExecutionPointer
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                StepId = stepDefinitionId,
                Status = ExecutionPointerStatus.Running,
            };
            
            ExecutionPointers.Add(executionPointer);
            return executionPointer;
        }

        public ExecutionPointer GetNextExecutionPointer()
        {
            return ExecutionPointers.LastOrDefault();
        }

        public void Complete()
        {
            if (Status != WorkflowStatus.Running)
            {
                throw new InvalidOperationException("Cannot complete a no-running workflow");
            }

            Status = WorkflowStatus.Completed;
        }
    }

    public interface IWorkflowExecutor
    {
        Task ExecuteWorkflow(WorkflowDefinition definition, Workflow workflow);
    }

    public class WorkflowExecutor : IWorkflowExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public WorkflowExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteWorkflow(WorkflowDefinition definition, Workflow workflow)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                if (workflow.Status == WorkflowStatus.Running)
                {
                    var next = workflow.GetNextExecutionPointer();
                    if (next == null)
                    {
                        workflow.Complete();
                    }
                    else
                    {
                        var context = new StepExecutionContext
                        {
                            WorkflowDefinition = definition,
                            Workflow = workflow,
                            ExecutionPointer = next
                        };

                        await ExecuteExecutionPointer(scope, context);
                    }
                }
            }
        }

        public async Task ExecuteExecutionPointer(IServiceScope scope, IStepExecutionContext context)
        {
            var step = BuildStep(scope, context.WorkflowDefinition, context.ExecutionPointer);
            var result = await step.RunAsync(context);

            if (result.Proceed)
            {
                context.ExecutionPointer.Complete();
                    
                
                var nextStepId = result.NextStepId ?? context.WorkflowDefinition.GetNextStepId(context.ExecutionPointer.StepId);
                if (nextStepId != null)
                {
                    var newContext = new StepExecutionContext
                    {
                        Workflow = context.Workflow,
                        ExecutionPointer = context.Workflow.StartStep(nextStepId),
                        WorkflowDefinition = context.WorkflowDefinition
                    };
                    await ExecuteExecutionPointer(scope, newContext);
                }
                else
                {
                    context.Workflow.Complete();
                }
            }
            else
            {
                context.ExecutionPointer.Status = ExecutionPointerStatus.Running;
            }
        }

        private IStepBody BuildStep(IServiceScope scope, WorkflowDefinition definition, ExecutionPointer executionPointer)
        {
            var stepDefinition = definition.Steps.SingleOrDefault(x => x.Id == executionPointer.StepId);
            return (IStepBody) scope.ServiceProvider.GetService(stepDefinition.BodyType);
        }
    }

    public class ExecutionPointer
    {
        public Guid Id { get; set; }
        public string StepId { get; set; }
        public ExecutionPointerStatus Status { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartTime { get; set; }

        public void Complete()
        {
            Status = ExecutionPointerStatus.Completed;
            EndTime = DateTime.Now;
        }
    }

    public interface IWorkflowEngine
    {
        Task<string> StartWorkflow(string workflowDefinitionId, int version = 0);
        Task PublishActionEvent();
        Task RegisterWorkflowDefinition(string name, WorkflowDefinition definition);
    }

    public class WorkflowEngine : IWorkflowEngine
    {
        private readonly IWorkflowRepository _repository;
        private readonly IWorkflowExecutor _executor;

        public WorkflowEngine(IWorkflowRepository repository, IWorkflowExecutor executor)
        {
            _repository = repository;
            _executor = executor;
        }


        public async Task<string> StartWorkflow(string workflowDefinitionId, int version = 0)
        {
            var definition = await _repository.GetWorkflowDefinition(workflowDefinitionId, version);
            var workflowId = Guid.NewGuid().ToString();
            var workflow = BuildWorkflow(workflowId, definition);
            workflow.StartStep(definition.Steps.First().Id);
            _executor.ExecuteWorkflow(definition, workflow);
            return workflowId;
        }

        private Workflow BuildWorkflow(string workflowId, WorkflowDefinition definition)
        {
            return new Workflow(workflowId,definition);
        }

        public Task PublishActionEvent()
        {
            throw new NotImplementedException();
        }

        public Task RegisterWorkflowDefinition(string name, WorkflowDefinition definition)
        {
            return _repository.AddWorkflowDefinition(name, definition);
        }
    }

    public interface IWorkflowRepository
    {
        Task AddWorkflowDefinition(string workflowDefinitionId, WorkflowDefinition definition);
        Task<WorkflowDefinition> GetWorkflowDefinition(string workflowDefinitionId, int version);
    }

    public class WorkflowRepository : IWorkflowRepository
    {
        IDictionary<string, List<WorkflowDefinition>> _definitions = new Dictionary<string, List<WorkflowDefinition>>();

        public Task AddWorkflowDefinition(string workflowDefinitionId, WorkflowDefinition definition)
        {
            if (!_definitions.ContainsKey(workflowDefinitionId))
            {
                _definitions.Add(workflowDefinitionId, new List<WorkflowDefinition>());
            }

            _definitions[workflowDefinitionId].Add(definition);
            return Task.CompletedTask;
        }

        public Task<WorkflowDefinition> GetWorkflowDefinition(string workflowDefinitionId, int version)
        {
            var definitions = _definitions[workflowDefinitionId];
            var definition = version > 0 ? definitions[version - 1] : definitions.Last();
            return Task.FromResult(definition);
        }
    }

    public class WorkflowActionEvent
    {
    }

    public class WorkflowDefinitionBuilder
    {
        private List<Action<WorkflowDefinition>> _actions = new List<Action<WorkflowDefinition>>();

        public WorkflowDefinitionBuilder Name(string workflowDefinitionName)
        {
            _actions.Add(x => x.Description = workflowDefinitionName);
            return this;
        }

        public WorkflowDefinition Build()
        {
            var definition = new WorkflowDefinition();
            _actions.ForEach(action => action(definition));
            return definition;
        }

        public StepDefinitionBuilder AddStep<StepBody>(string stepId) where StepBody : IStepBody
        {
            return new StepDefinitionBuilder(this, typeof(StepBody)).Id(stepId);
        }


        public class StepDefinitionBuilder
        {
            private readonly WorkflowDefinitionBuilder _workflowDefinitionBuilder;
            private StepDefinition _stepDefinition;


            public StepDefinitionBuilder(WorkflowDefinitionBuilder workflowDefinitionBuilder, Type bodyType)
            {
                _workflowDefinitionBuilder = workflowDefinitionBuilder;
                _stepDefinition = new StepDefinition
                {
                    BodyType = bodyType
                };
                _workflowDefinitionBuilder._actions.Add(x => x.Steps.Add(_stepDefinition));
            }

            public StepDefinitionBuilder Id(string stepId)
            {
                _stepDefinition.Id = stepId;
                return this;
            }
        }
    }
}