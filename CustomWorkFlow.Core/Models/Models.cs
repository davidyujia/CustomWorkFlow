using System;

namespace CustomWorkFlow.Core.Models
{
    public class WorkFlowProcessModel
    {
    }

    public class WorkFlowModel
    {
    }

    public class WorkFlowProcessStageMessageModel
    {
        public Guid ProcessId { get; set; }
        public Guid StageId { get; set; }
        public Guid AssignId { get; set; }
        public object UserId { get; set; }
        public bool IsAccept { get; set; }
        public string Message { get; set; }
    }
}
