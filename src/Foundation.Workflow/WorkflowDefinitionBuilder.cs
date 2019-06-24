using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Foundation.Workflow
{
    public class WorkflowDefinitionBuilder
    {
        private List<Action<WorkflowDefinition>> _actions = new List<Action<WorkflowDefinition>>();

        public WorkflowDefinitionBuilder(string id, int version = 1)
        {
            _actions.Add(def =>
            {
                def.Id = id;
                def.Version = version;
            });
        }
        public WorkflowDefinitionBuilder Name(string workflowDefinitionName)
        {
            _actions.Add(x => x.Description = workflowDefinitionName);
            return this;
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
            private StepDefinition _stepDefinition;


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

            public EventActionBuilder ForAction(string actionName, params Guid[] actorIds)
            {
                var action = new EventActionDefinition(actionName,actorIds);
                _stepDefinition.Actions.Add(action);
                return new EventActionBuilder(this, action);
            }
            
            public class EventActionBuilder
            {
                private readonly StepDefinitionBuilder _stepDefinitionBuilder;
                private readonly EventActionDefinition _eventActionDefinition;

                public EventActionBuilder(StepDefinitionBuilder stepDefinitionBuilder, EventActionDefinition eventActionDefinition)
                {
                    _stepDefinitionBuilder = stepDefinitionBuilder;
                    _eventActionDefinition = eventActionDefinition;
                }

                public StepDefinitionBuilder Returns(ExecutionResult executionResult)
                {
                    _eventActionDefinition.Action = new ReturnsEventAction(executionResult);
                    return _stepDefinitionBuilder;
                }

                public StepDefinitionBuilder Do(Expression<Func<IStepExecutionContext, ExecutionResult>> expression)
                {
                    _eventActionDefinition.Action = new ExpressionEventAction(expression);
                    return _stepDefinitionBuilder;
                }
            }
        }
    }
}