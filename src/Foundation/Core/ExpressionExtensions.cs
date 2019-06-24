using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Foundation.Core
{
    public static class ExpressionExtensions
    {
        public static MethodInfo ExtractMethodInfo(this LambdaExpression lambdaExpression)
        {
            return (lambdaExpression.Body as MethodCallExpression)?.Method 
                ?? throw new InvalidOperationException("LambdaExpression body must be a MethodCallExpression");
        }
    }
}
