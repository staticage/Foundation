using System;
using System.Collections.Generic;

namespace Foundation.Workflow
{
    public class ExecutionPointer
    {
        public Guid Id { get; set; }
        public string StepId { get; set; }
        public PointerStatus Status { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public List<WorkflowActionEvent> PublishedEvents { get; set; } = new List<WorkflowActionEvent>();
        public string EventName { get; set; }
        public string EventKey { get; set; }
        // public IDictionary<string, string> ExtensionAttributes { get; set; }
        // public object Form { get; set; }

        public void Complete()
        {
            EndTime = DateTime.Now;
            Status = PointerStatus.Complete;
        }
    }
}