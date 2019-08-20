using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Workflow
{
    public class Workflow
    {
        public Guid Id { get; set; }
        public List<ExecutionPointer> ExecutionPointers { get; set; } = new List<ExecutionPointer>();
        public WorkflowStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string WorkflowDefinitionName { get; set; }
        public int Version { get; set; }

        private Workflow()
        {
        }

        public static Workflow Start(Guid id, WorkflowDefinition definition)
        {
            var workflow = new Workflow
            {
                Id = id,
                StartTime = DateTime.Now,
                WorkflowDefinitionName = definition.Name,
                Version = definition.Version
            };
            return workflow;
        }

        public ExecutionPointer StartStep(string stepId)
        {
            var executionPointer = new ExecutionPointer
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                StepId = stepId,
                Status = ExecutionPointerStatus.Running,
            };

            ExecutionPointers.Add(executionPointer);
            return executionPointer;
        }

        public ExecutionPointer GetNextExecutionPointer()
        {
            return ExecutionPointers.LastOrDefault();
        }

        public void Complete()
        {
            if (Status != WorkflowStatus.Running)
            {
                throw new InvalidOperationException("Cannot complete a no-running workflow");
            }

            EndTime = DateTime.Now;
            Status = WorkflowStatus.Completed;
        }

        public ExecutionResult Obsolete()
        {
            if (Status != WorkflowStatus.Running)
            {
                throw new InvalidOperationException("Cannot obsolete a no-running workflow");
            }
            EndTime = DateTime.Now;
            Status = WorkflowStatus.Terminated;
            return ExecutionResult.Next();
        }
    }
}