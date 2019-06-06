using System;
using System.Collections.Generic;

namespace Foundation.Workflow
{
    public class ExecutionPointer
    {
        public Guid Id { get; set; }
        public string StepId { get; set; }
        public ExecutionPointerStatus Status { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public List<WorkflowActionEvent> PublishedEvents { get; set; }

        public void Complete()
        {
            EndTime = DateTime.Now;
            Status = ExecutionPointerStatus.Completed;
        }
    }
}