namespace Foundation.Workflow
{
    public class ReturnsEventAction : IEventAction
    {
        private readonly ExecutionResult _executionResult;

        public ReturnsEventAction(ExecutionResult executionResult)
        {
            _executionResult = executionResult;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _executionResult;
    }
}