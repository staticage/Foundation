using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Foundation.Workflow.Tests
{
    public class WorkflowEngineTests
    {
        private WorkflowDefinition definition;
        private IServiceProvider _provider;

        [SetUp]
        public void Setup()
        {
            var builder = new WorkflowDefinitionBuilder("Workflow1");
            builder.AddStep<UserActionStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E")
                .Input("Input1",() => _provider.GetService<IWorkflowPersistence>() )
                .ForAction("同意")
                .Returns(ExecutionResult.Next());
            definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(UserActionStepBody));

            var services = new ServiceCollection();
            services.AddWorkflow();
            services.AddScoped<UserActionStepBody>();
            services.AddDbContext<WorkflowDbContext>(opt =>
            {
                opt.UseSqlServer("Server=62.234.214.209,1445;Database=rebus_lyh;User Id=sa;Password=sasa@123;",
                    options => options.MigrationsAssembly(typeof(WorkflowDbContext).Assembly.FullName));
            });
            _provider = services.BuildServiceProvider();
        }


        [Test]
        public async Task Should_execute_steps_one_by_one()
        {
            IWorkflowHost engine = new WorkflowEngine(_provider.GetService<IWorkflowPersistence>(),
                new WorkflowExecutor(_provider));
            engine.Registrar.RegisterWorkflowDefinition(definition);

            var workflowId = await engine.StartWorkflow("Workflow1", new WorkflowData());
            var workflow = await GetService<IWorkflowPersistence>().GetWorkflowInstance(workflowId);
            workflow.Status.Should().Be(WorkflowStatus.Running);
            await engine.PublishActionEvent(workflowId, new WorkflowActionEvent
            {
                Action = "同意"
            });

            workflow = await GetService<IWorkflowPersistence>().GetWorkflowInstance(workflowId);
            workflow.Status.Should().Be(WorkflowStatus.Complete);
        }

        private T GetService<T>()
        {
            return _provider.CreateScope().ServiceProvider.GetService<T>();
        }

        [Test]
        public void When_add_a_new_step_it_should_contains_the_step()
        {
            var builder = new WorkflowDefinitionBuilder("Workflow1");
            builder.AddStep<UserActionStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E");
            var definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(UserActionStepBody));
        }

        [Test]
        public void WorkflowSpecTests()
        {
            IWorkflowHost engine = new WorkflowEngine(_provider.GetService<IWorkflowPersistence>(),
                new WorkflowExecutor(_provider));
            
            
            var services = new ServiceCollection();
            services.AddScoped<UserActionStepBody>();
            services.AddWorkflow();
            services.AddDbContext<WorkflowDbContext>(opt =>
            {
                opt.UseSqlServer("Server=62.234.214.209,1445;Database=rebus_lyh;User Id=sa;Password=sasa@123;",
                    options => options.MigrationsAssembly(typeof(WorkflowDbContext).Assembly.FullName));
            });

            using (var scope = _provider.CreateScope())
            {
                var spec = new WorkflowSpec
                {
                    Id = Guid.NewGuid(),
                    Version = 1,
                    StepSpecs = new List<WorkflowStepSpec>
                    {
                        new WorkflowStepSpec
                        {
                            RoleId = Guid.Parse("DEDC7F6D-6E59-4D8F-B014-0A598D179EEA"),
                            ActionSpecs = new List<StepActionSpec>
                            {
                                new StepActionSpec
                                {
                                    Action = WorkflowAction.Submit
                                }
                            }
                        }
                    }
                };
                
                var repository = scope.ServiceProvider.GetService<IWorkflowPersistence>();
                var definition = engine.Registrar.GetWorkflowDefinition(spec.Id.ToString(), spec.Version);
                if (definition == null)
                {
                    var builder = new WorkflowDefinitionBuilder(spec.Id.ToString(), spec.Version);
                    string startId = null;
                    string previousStepId = null;

                    for (var i = 0; i < spec.StepSpecs.Count; i++)
                    {
                        var stepSpec = spec.StepSpecs[i];
                        var stepId = Guid.NewGuid().ToString("N");
                        if (startId == null)
                        {
                            startId = stepId;
                        }
                        
                        var stepBuilder = builder.AddStep<UserActionStepBody>(stepId);
                        stepBuilder.Input("AssignedPrincipal", () => stepSpec.RoleId);
                        stepBuilder.Input("StepName", () => stepSpec.Name);
                        
                        var roleIds = new List<Guid>();
                        if (stepSpec.RoleId.HasValue)
                        {
                            roleIds.Add(stepSpec.RoleId.Value);
                        }
                        
                        foreach (var actionSpec in stepSpec.ActionSpecs)
                        {
                            var actionBuilder = stepBuilder.ForAction(actionSpec.Action.ToString(),roleIds.ToArray());
                            if (actionSpec.OutcomeType == OutcomeType.Startup)
                            {
                                actionBuilder.Returns(ExecutionResult.Next(startId));
                            }
                            else if (actionSpec.OutcomeType == OutcomeType.PreviousStep)
                            {
                                if (previousStepId == null)
                                {
                                    throw new InvalidOperationException("Cannot go to previous step.");
                                }
                                actionBuilder.Returns(ExecutionResult.Next(previousStepId));
                            }
                            else if (actionSpec.OutcomeType == OutcomeType.Next)
                            {
                                actionBuilder.Returns(ExecutionResult.Next());
                            }
                            else if (actionSpec.OutcomeType == OutcomeType.Obsolete)
                            {
                                actionBuilder.Do(context => context.Workflow.Obsolete());
                            }
                            else if (actionSpec.OutcomeType == OutcomeType.StepId)
                            {
                                throw new NotImplementedException();
                                //actionStep.Outcomes.Add(new StepOutcome{ExternalNextStepId = actionSpec.NextStepId});
                            }
                        }

                        stepBuilder.Name(stepSpec.Type.ToString());
                        previousStepId = stepId;
                    }

                    definition = builder.Build();
                    engine.Registrar.RegisterWorkflowDefinition(definition);
                }
            }
        }
    }

    public class WorkflowData
    {
        
    }
}