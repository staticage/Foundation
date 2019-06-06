using System;
using System.Linq.Expressions;

namespace Foundation.Workflow
{
    public class ExpressionEventAction : IEventAction
    {
        private readonly Expression<Func<IStepExecutionContext, ExecutionResult>> _expression;

        public ExpressionEventAction(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
        {
            _expression = expression;
        }

        public ExecutionResult Act(IStepExecutionContext context) => _expression.Compile()(context);
    }
}