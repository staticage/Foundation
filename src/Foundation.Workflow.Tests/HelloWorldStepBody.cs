using System;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Workflow.Tests
{
    
    
    public class HelloWorldStepBody : IStepBody
    {
        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            if (!context.ExecutionPointer.PublishedEvents.Any())
            {
                return ExecutionResult.WaitForEvent(context.ExecutionPointer.Id.ToString("N"));
            }
            
            var evt = context.ExecutionPointer.PublishedEvents.First();
            var action = context.StepDefinition.Actions.SingleOrDefault(x => x.ActionName == evt.Action);
            if (action == null)
            {
                throw new InvalidOperationException("无效的流程操作");
            }
            return await Task.FromResult(context.StepDefinition.Actions.Single(x => x.ActionName == evt.Action).Action.Act(context));
        }
    }
}