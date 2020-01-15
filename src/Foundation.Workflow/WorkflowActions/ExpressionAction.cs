using System;
using System.Linq.Expressions;
using WorkflowCore.Interface;

namespace Foundation.Workflow
{
    public class ExpressionAction : IWorkflowAction
    {
        private readonly Expression<Func<IStepExecutionContext, ExecutionResult>> _expression;

        public ExpressionAction(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
        {
            _expression = expression;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _expression.Compile()(context);
    }
}