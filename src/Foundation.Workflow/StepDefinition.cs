using System;
using System.Collections.Generic;

namespace Foundation.Workflow
{
    public class StepDefinition
    {
        public string Id { get; set; }
        public Type BodyType { get; set; }
        public List<EventActionDefinition> Actions { get; set; } = new List<EventActionDefinition>();
    }
}