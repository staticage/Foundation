using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;


namespace Foundation.Workflow
{
    public class ActionStepBody : StepBody
    {
        public Action<IStepExecutionContext> Body => Parameters["Body"] as Action<IStepExecutionContext>;
        
        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        { 
            Body(context);
            return Task.FromResult(ExecutionResult.Next());
        }
    }
}