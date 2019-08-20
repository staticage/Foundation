using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Foundation.Workflow
{
    public class WorkflowDefinitionBuilder
    {
        private readonly List<Action<WorkflowDefinition>> _actions = new List<Action<WorkflowDefinition>>();

        public WorkflowDefinitionBuilder(string name, int version = 1)
        {
            _actions.Add(def =>
            {
                def.Name = name;
                def.Version = version;
            });
        }
 
        public WorkflowDefinition Build()
        {
            var definition = new WorkflowDefinition();
            _actions.ForEach(action => action(definition));
            return definition;
        }

        public StepDefinitionBuilder AddStep<StepBody>(string stepId) where StepBody : IStepBody
        {
            return new StepDefinitionBuilder(this, typeof(StepBody)).Id(stepId);
        }


        public class StepDefinitionBuilder
        {
            private readonly WorkflowDefinitionBuilder _workflowDefinitionBuilder;
            private readonly StepDefinition _stepDefinition;


            public StepDefinitionBuilder(WorkflowDefinitionBuilder workflowDefinitionBuilder, Type bodyType)
            {
                _workflowDefinitionBuilder = workflowDefinitionBuilder;
                _stepDefinition = new StepDefinition
                {
                    BodyType = bodyType
                };
                _workflowDefinitionBuilder._actions.Add(x => x.Steps.Add(_stepDefinition));
            }

            public StepDefinitionBuilder Id(string stepId)
            {
                _stepDefinition.Id = stepId;
                return this;
            }
            
            public StepDefinitionBuilder Name(string stepName)
            {
                _stepDefinition.Name = stepName;
                return this;
            }
            
            public StepDefinitionBuilder Input(string paramName, object value)
            {
                _stepDefinition.Parameters.Add(paramName, () => value);
                return this;
            }

            public StepDefinitionBuilder Input(string paramName, Expression<Func<object>> valueExpression)
            {
                _stepDefinition.Parameters.Add(paramName, valueExpression.Compile());
                return this;
            }
            
            public WorkflowActionBuilder ForAction(string actionName, params Guid[] roleIds)
            {
                var action = new WorkflowActionDefinition(actionName,roleIds);
                _stepDefinition.Actions.Add(action);
                return new WorkflowActionBuilder(this, action);
            }
            
            public class WorkflowActionBuilder
            {
                private readonly StepDefinitionBuilder _stepDefinitionBuilder;
                private readonly WorkflowActionDefinition _eventActionDefinition;

                public WorkflowActionBuilder(StepDefinitionBuilder stepDefinitionBuilder, WorkflowActionDefinition eventActionDefinition)
                {
                    _stepDefinitionBuilder = stepDefinitionBuilder;
                    _eventActionDefinition = eventActionDefinition;
                }

                public StepDefinitionBuilder Returns(ExecutionResult executionResult)
                {
                    _eventActionDefinition.Action = new ReturnsExecutionResultAction(executionResult);
                    return _stepDefinitionBuilder;
                }

                public StepDefinitionBuilder Do(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
                {
                    _eventActionDefinition.Action = new ExpressionAction(expression);
                    return _stepDefinitionBuilder;
                }
            }
        }
    }
}