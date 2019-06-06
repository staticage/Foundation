namespace Foundation.Workflow
{
    public class ExecutionResult
    {
        public bool Proceed { get; set; }
        public string EventName { get; set; }
        public string EventKey { get; set; }
        public string NextStepId { get; set; }

        public static ExecutionResult Next(string nextStepId = null)
        {
            return new ExecutionResult {Proceed = true, NextStepId = nextStepId};
        }

        public static ExecutionResult WaitForEvent(string eventName, string eventKey)
        {
            return new ExecutionResult
            {
                EventName = eventName,
                EventKey = eventKey
            };
        }
    }
}