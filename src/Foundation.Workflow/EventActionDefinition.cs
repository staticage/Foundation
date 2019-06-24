using System;

namespace Foundation.Workflow
{
    public class EventActionDefinition
    {
        public string ActionName { get; private set; }
        public Guid[] ActorIds { get; }
        public IEventAction Action { get; set; }

        public EventActionDefinition(string actionName,params Guid[] actorIds)
        {
            ActionName = actionName;
            ActorIds = actorIds;
        }
    }
}