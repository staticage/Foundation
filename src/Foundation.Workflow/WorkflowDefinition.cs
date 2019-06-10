using System;
using System.Collections.Generic;

namespace Foundation.Workflow
{
    public class WorkflowDefinition
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public List<StepDefinition> Steps { get; set; } = new List<StepDefinition>();

        public string GetNextStepId(string currentStepId)
        {
            var nextIndex = Steps.FindIndex(x => x.Id == currentStepId) + 1;
            return nextIndex < Steps.Count ? Steps[nextIndex].Id : null;
        }
    }


    public enum WorkflowStatus
    {
        Running = 0,
        Completed = 1,
        Terminated = 2
    }

    public interface IWorkflowDefinitionRegistry
    {
    }

    public class WorkflowActionEvent
    {
        public Guid ActorId { get; set; }
        public string Action { get; set; }
    }
}