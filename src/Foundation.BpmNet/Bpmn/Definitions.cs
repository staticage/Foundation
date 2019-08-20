using System.Collections.Generic;

namespace Foundation.BpmNet.Bpmn
{
    public class Definitions
    {
        public string Id { get; set; }
        private List<Process> ProcessDefinitions { get; set; }
    }

    public class Process
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Event StartEvent { get; set; }
        public Event EndEvent { get; set; }
    }

    public class Event
    {
        public string Id { get; set; }
    }
}