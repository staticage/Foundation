using System;
using System.Threading.Tasks;

namespace Foundation.Workflow.Tests
{
    
    
    public class HelloWorldStepBody : IStepBody
    {
        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            await Task.CompletedTask;
            return ExecutionResult.Next();
        }
    }
}