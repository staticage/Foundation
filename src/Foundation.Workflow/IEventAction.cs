namespace Foundation.Workflow
{
    public interface IEventAction
    {
        ExecutionResult Act(IStepExecutionContext context);
    }
}