using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Workflow
{
    public interface IWorkflowVariable
    {
        bool TryGetVariable(string name, out object value);
        void SetVariable(string name, object value);
    }

    public static class WorkflowVariableExtensions
    {
        public static T GetVariable<T>(this IWorkflowVariable @this, string name, T defaultValue)
        {
            var value = defaultValue as object;
            return (T) (@this.TryGetVariable(name, out value) ? value : defaultValue);
        }

        public static bool GetBooleanVariable(this IWorkflowVariable @this,string name, bool defaultValue = false) => @this.GetVariable(name, defaultValue);
        public static int GetInt32Variable(this IWorkflowVariable @this,string name, int defaultValue = 0) => @this.GetVariable(name, defaultValue);
        
    }

    public interface IStepBody : IWorkflowVariable
    {
        Task<ExecutionResult> RunAsync(IStepExecutionContext context);
    }

    public abstract class StepBody : IStepBody, IWorkflowVariable
    {
        public abstract Task<ExecutionResult> RunAsync(IStepExecutionContext context);
        private IDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public bool TryGetVariable(string name, out object value)
        {
            return Parameters.TryGetValue(name, out value);
        }

        public void SetVariable(string name, object value)
        {
            Parameters[name] = value;
        }
    }
}