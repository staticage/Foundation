using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IStepBody
    {
        Task<ExecutionResult> RunAsync(IStepExecutionContext context);
    }

    public abstract class StepBody : IStepBody
    {
        public abstract Task<ExecutionResult> RunAsync(IStepExecutionContext context);
        protected IDictionary<string,object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}