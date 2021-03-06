using Foundation.Workflow;

namespace WorkflowCore.Interface
{
    public interface IStepExecutionContext
    {
        WorkflowDefinition WorkflowDefinition { get; }
        WorkflowInstance Workflow { get; }
        ExecutionPointer ExecutionPointer { get; }
        StepDefinition StepDefinition { get; } 
    }
}