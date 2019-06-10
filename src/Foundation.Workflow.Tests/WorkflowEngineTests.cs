using System;
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
            builder.AddStep<HelloWorldStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E").ForAction("同意").Returns(ExecutionResult.Next());
            definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(HelloWorldStepBody));

            var services = new ServiceCollection();
            services.AddScoped<HelloWorldStepBody>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
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
            IWorkflowEngine engine = new WorkflowEngine(_provider.GetService<IWorkflowRepository>(), new WorkflowExecutor(_provider));
            engine.Registrar.RegisterWorkflowDefinition(definition);

            var workflowId =  await engine.StartWorkflow("Workflow1");
            var workflow = await GetService<IWorkflowRepository>().GetWorkflow(workflowId);
            workflow.Status.Should().Be(WorkflowStatus.Running);
            await engine.PublishActionEvent(workflowId, new WorkflowActionEvent
            {
                Action = "同意"
            });
            
            workflow = await GetService<IWorkflowRepository>().GetWorkflow(workflowId);
            workflow.Status.Should().Be(WorkflowStatus.Completed);
        }

        private T GetService<T>()
        {
            return _provider.CreateScope().ServiceProvider.GetService<T>();
        }

        [Test]
        public void When_add_a_new_step_it_should_contains_the_step()
        {
            var builder = new WorkflowDefinitionBuilder("Workflow1");
            builder.AddStep<HelloWorldStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E");
            var definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(HelloWorldStepBody));
        }
    }
}