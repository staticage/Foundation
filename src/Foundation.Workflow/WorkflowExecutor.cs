using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Workflow
{
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

        public async Task ExecuteWorkflowAction(WorkflowDefinition definition, Guid workflowId, WorkflowActionEvent evt)
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

                if (context.Workflow.Status == WorkflowStatus.Running)
                {
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
            }
            else
            {
                context.ExecutionPointer.Status = ExecutionPointerStatus.Running;
                context.ExecutionPointer.EventName = result.EventName;
                context.ExecutionPointer.EventKey = result.EventKey;
            }
        }

        private IStepBody BuildStep(IServiceScope scope, WorkflowDefinition definition, ExecutionPointer executionPointer)
        {
            var stepDefinition = definition.Steps.Single(x => x.Id == executionPointer.StepId);
            return (IStepBody) scope.ServiceProvider.GetService(stepDefinition.BodyType);
        }
    }
}