using System.Linq;

namespace Foundation.Workflow
{
    public class StepExecutionContext : IStepExecutionContext
    {
        public WorkflowDefinition WorkflowDefinition { get; set; }
        public Workflow Workflow { get; set; }
        public ExecutionPointer ExecutionPointer { get; set; }
        public StepDefinition StepDefinition => WorkflowDefinition.Steps.Single(x => x.Id == ExecutionPointer.StepId);
    }
}