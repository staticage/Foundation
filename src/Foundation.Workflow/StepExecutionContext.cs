using System.Linq;
using WorkflowCore.Interface;

namespace Foundation.Workflow
{
    public class StepExecutionContext : IStepExecutionContext
    {
        public WorkflowDefinition WorkflowDefinition { get; set; }
        public WorkflowInstance Workflow { get; set; }
        public ExecutionPointer ExecutionPointer { get; set; }
        public StepDefinition StepDefinition => WorkflowDefinition.Steps.Single(x => x.Id == ExecutionPointer.StepId);
    }
}