using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WorkflowCore;

namespace Foundation.Workflow.Tests
{

    
    public enum WorkflowAction
    {
        [Description("同意")] Approve = 1,
        [Description("不同意")] Reject = 2,
        //[Description("退回")] SendBack=3,
        [Description("提交")] Submit = 4,
        [Description("作废")] Obsolete = 5,
        [Description("打印")] Print = 6,
        [Description("撤回")] Undo = 7,
        [Description("办结")] Complete = 8,
        [Description("放弃")] GiveUp = 9,
    }
    
    public class WorkflowSpec 
    {
        public string Name { get; private set; }
        public List<WorkflowStepSpec> StepSpecs { get; set; } = new List<WorkflowStepSpec>();
        public string TriggeredEventType { get; set; }
        public WorkflowBusinessType WorkflowBusinessType { get; set; }
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
    
    public enum WorkflowBusinessType
    {
        等候入住函 = 0,
        入住协议 = 1,
        评估报告 = 2,
        评估预约 = 3,
    }
    
    public class WorkflowStepSpec
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public StepBusinessType Type { get; set; }
        public Guid? RoleId { get; set; } 
        public List<StepActionSpec> ActionSpecs { get; set; } = new List<StepActionSpec>();
    }
    
    public enum StepBusinessType
    {
        新增 = 0,
        审批 = 1,
        财务收费 = 2,
        客户线下签字 = 3,
        盖章 = 4,
        送达 = 5,
        归档 = 6,
        打印 = 7,
        合同签署 = 8
    }
    
    public enum OutcomeType
    {
        Next = 0,
        Startup = 1,
        PreviousStep = 2,
        StepId = 3,
        Obsolete = 4
    }
    
    public class StepActionSpec
    {
        public WorkflowAction Action { get; set; }

        public OutcomeType OutcomeType
        {
            get
            {
                if (Action == WorkflowAction.Approve || Action == WorkflowAction.Submit || Action == WorkflowAction.Complete)
                {
                    return OutcomeType.Next;
                }
                else if (Action == WorkflowAction.Reject || Action == WorkflowAction.Undo)
                {
                    return OutcomeType.Startup;
                }
                else if (Action == WorkflowAction.Reject || Action == WorkflowAction.Undo)
                {
                    return OutcomeType.Startup;
                }
                else if (Action == WorkflowAction.Obsolete)
                {
                    return OutcomeType.Obsolete;
                }
                else
                {
                    return OutcomeType.Next;
                    //throw new NotImplementedException($"Invalid action type {Action.ToString()}");
                }
            }
        }

        public string NextStepId { get; set; }
    }
}