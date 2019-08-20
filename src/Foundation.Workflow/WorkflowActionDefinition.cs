using System;

namespace Foundation.Workflow
{
    public class WorkflowActionDefinition
    {
        public string Name { get; private set; }
        public Guid[] RoleIds { get; }
        public IWorkflowAction Action { get; set; }

        public WorkflowActionDefinition(string actionName,params Guid[] roleIds)
        {
            Name = actionName;
            RoleIds = roleIds;
        }
    }
}