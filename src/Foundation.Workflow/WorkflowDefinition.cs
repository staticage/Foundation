using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Foundation.Workflow
{
    public class WorkflowDefinition
    {
        public string Name { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public List<StepDefinition> Steps { get; set; } = new List<StepDefinition>();
        public string Id { get; set; }

        public string GetNextStepId(string currentStepId)
        {
            var nextIndex = Steps.FindIndex(x => x.Id == currentStepId) + 1;
            return nextIndex < Steps.Count ? Steps[nextIndex].Id : null;
        }
    }


    public enum WorkflowStatus
    {
        Runnable = 0,
        Complete = 1,
        Terminated = 2,
        Obsoleted = 3,
    }

    public interface IWorkflowDefinitionRegistry
    {
    }

    public class WorkflowActionEvent
    {
        public object Attributes { get; set; }
        
        public Guid ExecutionPointerId { get; set; }
        public Guid? ActorId { get; set; }
        public string Action { get; set; }
        public JObject ActionData { get; set; }
        public Guid WorkflowId { get; set; }
        public string Reason { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public DateTime EventTime { get; set; }
        public string CurrentStepId { get; set; }
        
    }
}