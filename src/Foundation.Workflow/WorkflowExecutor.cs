using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore;
using WorkflowCore.Interface;
using WorkflowCore.Models.LifeCycleEvents;

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
                var workflow = await scope.ServiceProvider.GetService<IPersistenceProvider>()
                    .GetWorkflowInstance(workflowId);
                if (workflow.Status == WorkflowStatus.Runnable)
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
                await scope.ServiceProvider.GetService<WorkflowEngineDbContext>().SaveChangesAsync();
            }
        }

        public async Task ExecuteWorkflowAction(WorkflowDefinition definition, Guid workflowId, WorkflowActionEvent evt)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var workflow = await scope.ServiceProvider.GetService<IPersistenceProvider>()
                    .GetWorkflowInstance(workflowId);
                if (workflow.Status == WorkflowStatus.Runnable)
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
                
                if (workflow.Status != WorkflowStatus.Runnable)
                {
                    LifeCycleEvent workflowLifeCycleEvent;
                    if (workflow.Status == WorkflowStatus.Complete)
                    {
                        workflowLifeCycleEvent = new WorkflowCompleted();
                    }
                    else if (workflow.Status == WorkflowStatus.Obsoleted)
                    {
                        workflowLifeCycleEvent = new WorkflowObsoleted();
                    }
                    else 
                    {
                        workflowLifeCycleEvent = new WorkflowTerminated();
                    }

                    workflowLifeCycleEvent.Reference = workflow.Reference;
                    workflowLifeCycleEvent.Version = workflow.Version;
                    workflowLifeCycleEvent.EventTimeUtc = DateTime.UtcNow;
                    workflowLifeCycleEvent.WorkflowDefinitionId = workflow.WorkflowDefinitionId;
                    workflowLifeCycleEvent.WorkflowInstanceId = workflow.Id.ToString();

                    scope.ServiceProvider.GetService<ILifeCycleEventPublisher>().Publish(workflowLifeCycleEvent);
                }
                
                await scope.ServiceProvider.GetService<WorkflowEngineDbContext>().SaveChangesAsync();
                await scope.ServiceProvider.GetService<ILifeCycleEventPublisher>().Accept();
            }
        }


        public async Task ExecuteExecutionPointer(IServiceScope scope, IStepExecutionContext context)
        {
            var step = BuildStep(scope, context.WorkflowDefinition, context.ExecutionPointer);
            
            scope.ServiceProvider.GetService<ILifeCycleEventPublisher>().Publish(new StepStarted
            {
                EventTimeUtc = DateTime.UtcNow,
                ExecutionPointerId = context.ExecutionPointer.Id.ToString(),
                Reference = context.Workflow.Reference,
                StepId = context.ExecutionPointer.StepId,
                Version = context.Workflow.Version,
                WorkflowDefinitionId = context.Workflow.WorkflowDefinitionId,
                WorkflowInstanceId = context.Workflow.Id.ToString()
            });
            
            var result = await step.RunAsync(context);

            if (result.Proceed)
            {
                context.ExecutionPointer.Complete();
                scope.ServiceProvider.GetService<ILifeCycleEventPublisher>().Publish(new StepCompleted
                {
                    EventTimeUtc = DateTime.UtcNow,
                    ExecutionPointerId = context.ExecutionPointer.Id.ToString(),
                    Reference = context.Workflow.Reference,
                    StepId = context.ExecutionPointer.StepId,
                    Version = context.Workflow.Version,
                    WorkflowDefinitionId = context.Workflow.WorkflowDefinitionId,
                    WorkflowInstanceId = context.Workflow.Id.ToString()
                });
                
                if (context.Workflow.Status == WorkflowStatus.Runnable)
                {
                    var nextStepId = result.NextStepId ??
                                     context.WorkflowDefinition.GetNextStepId(context.ExecutionPointer.StepId);
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
                context.ExecutionPointer.Status = PointerStatus.Running;
                context.ExecutionPointer.EventName = result.EventName;
                context.ExecutionPointer.EventKey = result.EventKey;
            }
        }

        private IStepBody BuildStep(IServiceScope scope, WorkflowDefinition definition,
            ExecutionPointer executionPointer)
        {
            var stepDefinition = definition.Steps.Single(x => x.Id == executionPointer.StepId);
            var step = (IStepBody) scope.ServiceProvider.GetService(stepDefinition.BodyType);
            foreach (var variable in stepDefinition.Variables)
            {
                step.SetVariable(variable.Key, variable.Value());
            }

            return step;
        }
    }
}