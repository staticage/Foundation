using System;
using System.Collections.Generic;
using System.Linq;
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
                    
                    if (next == null)
                    {
                        workflow.Complete();
                    }
                    else
                    {
                        next.PublishedEvents.Add(evt);
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

    public interface IWorkflowDefinitionRegistry
    {
    }

    public class WorkflowActionEvent
    {
    }
}