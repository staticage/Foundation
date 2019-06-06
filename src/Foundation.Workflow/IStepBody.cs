using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IStepBody
    {
        Task<ExecutionResult> RunAsync(IStepExecutionContext context);
    }
}