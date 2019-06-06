namespace Foundation.Workflow
{
    public class EventActionDefinition
    {
        public string ActionName { get; private set; }
        public IEventAction Action { get; set; }

        public EventActionDefinition(string actionName)
        {
            ActionName = actionName;
        }
    }
}