using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Foundation.Workflow.Tests
{
    public class UserActionStepBody : StepBody
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            if (!context.ExecutionPointer.PublishedEvents.Any())
            {
                return ExecutionResult.WaitForActionEvent(context.ExecutionPointer.Id.ToString("N"));
            }

            if (TryGetVariable("Input1",out object input1))
            {
                Console.WriteLine(input1?.ToString());
            }
            
            var evt = context.ExecutionPointer.PublishedEvents.First();
            var action = context.StepDefinition.Actions.SingleOrDefault(x => x.Name == evt.Action);
            if (action == null)
            {
                throw new InvalidOperationException("无效的流程操作");
            }
            return await Task.FromResult(context.StepDefinition.Actions.Single(x => x.Name == evt.Action).Action.Act(context));
        }
    }
}