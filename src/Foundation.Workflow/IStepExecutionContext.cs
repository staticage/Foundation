namespace Foundation.Workflow
{
    public interface IStepExecutionContext
    {
        WorkflowDefinition WorkflowDefinition { get; }
        Workflow Workflow { get; }
        ExecutionPointer ExecutionPointer { get; }
    }
}