using WorkflowCore.Interface;

namespace Foundation.Workflow
{
    public class ReturnsExecutionResultAction : IWorkflowAction
    {
        private readonly ExecutionResult _executionResult;

        public ReturnsExecutionResultAction(ExecutionResult executionResult)
        {
            _executionResult = executionResult;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _executionResult;
    }
}