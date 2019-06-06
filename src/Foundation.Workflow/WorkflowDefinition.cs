using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Workflow
{
    public class WorkflowDefinition
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public List<StepDefinition> Steps { get; set; } = new List<StepDefinition>();

        public string GetNextStepId(string currentStepId)
        {
            var nextIndex = Steps.FindIndex(x => x.Id == currentStepId) + 1;
            return nextIndex < Steps.Count ? Steps[nextIndex].Id : null;
        }
    }

    public class StepExecutionContext : IStepExecutionContext
    {
        public WorkflowDefinition WorkflowDefinition { get; set; }
        public Workflow Workflow { get; set; }
        public ExecutionPointer ExecutionPointer { get; set; }
    }


    public enum ExecutionPointerStatus
    {
        Running = 0,
        Completed
    }

    public enum WorkflowStatus
    {
        Running = 0,
        Completed = 1,
        Terminated = 2
    }

    public class Workflow
    {
        public Guid Id { get; set; }
        public List<ExecutionPointer> ExecutionPointers { get; set; } = new List<ExecutionPointer>();
        public WorkflowStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string WorkflowDefinitionId { get; set; }
        public int Version { get; set; }

        private Workflow()
        {
        }

        public static Workflow Start(Guid id, WorkflowDefinition definition)
        {
            var workflow = new Workflow
            {
                Id = id,
                StartTime = DateTime.Now,
                WorkflowDefinitionId = definition.Id,
                Version = definition.Version
            };
            return workflow;
        }

        public ExecutionPointer StartStep(string stepDefinitionId)
        {
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

            EndTime = DateTime.Now;
            Status = WorkflowStatus.Completed;
        }
    }

    public interface IWorkflowExecutor
    {
        Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId);
        Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId, WorkflowActionEvent evt);
    }

    public class WorkflowExecutor : IWorkflowExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public WorkflowExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var workflow = await scope.ServiceProvider.GetService<IWorkflowRepository>().GetWorkflow(workflowId);
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

                await scope.ServiceProvider.GetService<WorkflowDbContext>().SaveChangesAsync();
            }
        }

        public async Task ExecuteWorkflow(WorkflowDefinition definition, Guid workflowId, WorkflowActionEvent evt)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var workflow = await scope.ServiceProvider.GetService<IWorkflowRepository>().GetWorkflow(workflowId);
                if (workflow.Status == WorkflowStatus.Running)
                {
                    var next = workflow.GetNextExecutionPointer();
                    next.PublishedEvents.Add(evt);
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

                await scope.ServiceProvider.GetService<WorkflowDbContext>().SaveChangesAsync();
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

    public interface IWorkflowEngine
    {
        Task<Guid> StartWorkflow(string name, int version = 0);
        Task PublishActionEvent(Guid workflowId, WorkflowActionEvent evt);
        Task RegisterWorkflowDefinition(string name, WorkflowDefinition definition);
    }

    public interface IWorkflowRepository
    {
        Task AddWorkflowDefinition(string workflowDefinitionId, WorkflowDefinition definition);
        Task<WorkflowDefinition> GetWorkflowDefinition(string name, int version);
        Task AddWorkflow(Workflow workflow);
        Task<Workflow> GetWorkflow(Guid workflowId);
    }

    public interface IWorkflowDefinitionRegistry
    {
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

            public EventActionBuilder ForAction(string actionName)
            {
                var action = new EventActionDefinition(actionName);
                _stepDefinition.Actions.Add(action);
                return new EventActionBuilder(this, action);
            }
        }

        public class EventActionBuilder
        {
            private readonly StepDefinitionBuilder _stepDefinitionBuilder;
            private readonly EventActionDefinition _eventActionDefinition;

            public EventActionBuilder(StepDefinitionBuilder stepDefinitionBuilder, EventActionDefinition eventActionDefinition)
            {
                _stepDefinitionBuilder = stepDefinitionBuilder;
                _eventActionDefinition = eventActionDefinition;
            }

            public StepDefinitionBuilder Returns(ExecutionResult executionResult)
            {
                _eventActionDefinition.Action = new ReturnsEventAction(executionResult);
                return _stepDefinitionBuilder;
            }

            public StepDefinitionBuilder Do(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
            {
                _eventActionDefinition.Action = new ExpressionEventAction(expression);
                return _stepDefinitionBuilder;
            }
        }
    }

    public class EventActionDefinition
    {
        public string ActionName { get; private set; }
        public IEventAction Action { get; set; }

        public EventActionDefinition(string actionName)
        {
            ActionName = actionName;
        }
    }

    public interface IEventAction
    {
        ExecutionResult Act(IStepExecutionContext context);
    }

    public class ReturnsEventAction : IEventAction
    {
        private readonly ExecutionResult _executionResult;

        public ReturnsEventAction(ExecutionResult executionResult)
        {
            _executionResult = executionResult;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _executionResult;
    }

    public class ExpressionEventAction : IEventAction
    {
        private readonly Expression<Func<IStepExecutionContext, ExecutionResult>> _expression;

        public ExpressionEventAction(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
        {
            _expression = expression;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _expression.Compile()(context);
    }
}