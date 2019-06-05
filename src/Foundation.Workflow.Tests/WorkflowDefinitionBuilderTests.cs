using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Foundation.Workflow.Tests
{
    public class WorkflowDefinitionBuilderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_supports_definite_a_definition_name()
        {
            var builder = new WorkflowDefinitionBuilder();
            builder.Name("Test workflow");
            var definition = builder.Build();
            definition.Description.Should().Be("Test workflow");
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