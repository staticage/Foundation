namespace Foundation.Workflow
{
    public interface IWorkflowAction
    {
        ExecutionResult Act(IStepExecutionContext context);
    }
}