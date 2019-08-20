namespace Foundation.BpmNet
{
    public class WorkflowSpec
    {
    }

    public class Workflow
    {
        public WorkflowSpec Spec { get; }

        public Workflow(WorkflowSpec spec)
        {
            Spec = spec;
        }
    }

    public class TaskSpec
    {
        public WorkflowSpec WorkflowSpec { get; }
        public string Name { get; }

        protected TaskSpec(WorkflowSpec workflowSpec, string name)
        {
            WorkflowSpec = workflowSpec;
            Name = name;
        }
    }

    public class Cancel : TaskSpec
    {
        public bool Success { get; }

        public Cancel(WorkflowSpec workflowSpec, string name,  bool success = false) : base(workflowSpec, name)
        {
            Success = success;
        }
    }
}