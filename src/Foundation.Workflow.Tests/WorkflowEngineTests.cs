using System;
using System.Linq;
using FluentAssertions;
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
            var builder = new WorkflowDefinitionBuilder();
            builder.AddStep<HelloWorldStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(HelloWorldStepBody));

            var services = new ServiceCollection();
            services.AddScoped<HelloWorldStepBody>();
            _provider = services.BuildServiceProvider();
        }

        [Test]
        public void Should_execute_steps_one_by_one()
        {
            IWorkflowEngine engine = new WorkflowEngine(new WorkflowRepository(), new WorkflowExecutor(_provider));
            engine.RegisterWorkflowDefinition("Workflow1", definition);

            var workflowId =  engine.StartWorkflow("Workflow1");
        }

        [Test]
        public void When_add_a_new_step_it_should_contains_the_step()
        {
            var builder = new WorkflowDefinitionBuilder();
            builder.AddStep<HelloWorldStepBody>("A79047B7-1554-453B-8044-ABEF000F8B6E");
            var definition = builder.Build();
            definition.Steps.Single().Id.Should().Be("A79047B7-1554-453B-8044-ABEF000F8B6E");
            definition.Steps.Single().BodyType.Should().Be(typeof(HelloWorldStepBody));
        }
    }
}