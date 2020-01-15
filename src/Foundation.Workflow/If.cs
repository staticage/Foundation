using System;
using System.Linq;
using System.Threading.Tasks;


namespace Foundation.Workflow
{
//    public class If : StepBody
//    {
//        private Func<IStepExecutionContext ,bool> Condition => Parameters["Condition"] as Func<IStepExecutionContext ,bool>;
//        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
//        {
//            if (Condition(context))
//            {
//                return Task.FromResult(ExecutionResult.Next(context.WorkflowDefinition.GetChildSteps(context.Step.Id).FirstOrDefault()?.Id));
//            }
//
//            return Task.FromResult(ExecutionResult.Next());
//        }
//    }
}