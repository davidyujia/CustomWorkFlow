using CustomWorkFlow.Core.Models;
using System;
using System.Threading.Tasks;

namespace CustomWorkFlow.Core
{
    public class WorkFlowService
    {
        private readonly WorkFlowStoreBase _store;

        public WorkFlowService(WorkFlowStoreBase store)
        {
            _store = store;
        }

        public async Task<WorkFlowProcess> CreateProcessAsync(Guid workFlowId)
        {
            return await _store.CreateProcess(workFlowId);
        }

        public async Task<WorkFlowProcess> GetProcessAsync(Guid processId)
        {
            return await _store.GetProcessAsync(processId);
        }

        public async Task<WorkFlowProcess[]> GetProcessesAsync(params Guid[] processIds)
        {
            return await _store.GetProcessesAsync(processIds);
        }

        public async Task<WorkFlowProcess[]> GetAssignProcessesByUserIdAsync(object userId)
        {
            return await _store.GetAssignProcessesByUserIdAsync(userId);
        }
    }

    public class WorkFlow
    {
        public Guid Id { get; set; }
        public WorkFlowStage[] WorkFlowStages { get; set; }
    }

    public class WorkFlowStage
    {
        public Guid Id { get; }
        WorkFlowStageAssign[] Assign { get; }
    }

    public class WorkFlowStageAssign
    {
        public WorkFlowAssignType AssignIdType { get; }
        public Guid AssignId { get; }
    }

    public class WorkFlowProcess
    {
        private readonly WorkFlowStoreBase _store;

        public WorkFlowProcess(WorkFlowStoreBase store)
        {
            _store = store;
        }

        public Guid Id { get; set; }

        public WorkFlowProcessStatus Status { get; set; }

        public WorkFlowStage CurrentStage { get; set; }

        public WorkFlow WorkFlow { get; set; }

        private async Task SaveMessageAsync(Guid stageId, Guid assignId, bool isAccept, string message)
        {
            await _store.SaveMessageAsync(new WorkFlowProcessStageMessageModel
            {
                ProcessId = Id,
                StageId = stageId,
                AssignId = assignId,
                IsAccept = isAccept,
                Message = message
            });
        }

        public async Task AcceptAsync(Guid stageId, Guid assignId, string message)
        {
            await SaveMessageAsync(stageId, assignId, true, message);
        }

        public async Task RejectAsync(Guid stageId, Guid assignId, string message)
        {
            await SaveMessageAsync(stageId, assignId, false, message);
        }

        public async Task CancelAsync(string message)
        {
            await _store.CancelProcessAsync(Id, message);
        }
    }

    public class WorkFlowProcessStageMessage
    {
        public Guid ProcessId { get; set; }
        public Guid StageId { get; set; }
        public Guid AssignId { get; set; }
        public bool IsAccept { get; set; }
        public string Message { get; set; }
    }

    public enum WorkFlowProcessStatus
    {
        Init = 0,
        Process,
        Complate,
        Cancel
    }

    public enum WorkFlowAssignType
    {
        Dynamic = 0,
        User,
        Group,
    }
}
