using System;
using System.Collections.Generic;

namespace Foundation.Workflow
{
    public class StepDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Type BodyType { get; set; }
        public List<WorkflowActionDefinition> Actions { get; set; } = new List<WorkflowActionDefinition>();
        public Dictionary<string, Func<object>> Variables { get; set; } = new Dictionary<string, Func<object>>();
    }
}