using CustomWorkFlow.Core.Models;
using System;
using System.Threading.Tasks;

namespace CustomWorkFlow.Core
{
    public abstract class WorkFlowStoreBase
    {
        public abstract Task SaveMessageAsync(WorkFlowProcessStageMessageModel workFlowMessage);
        public abstract Task<WorkFlowProcess> CreateProcess(Guid workFlowId);
        public abstract Task<WorkFlowProcess> GetProcessAsync(Guid processId);
        public abstract Task<WorkFlowProcess[]> GetProcessesAsync(Guid[] processIds);
        public abstract Task CancelProcessAsync(Guid id, string message);
        public abstract Task<WorkFlowProcess[]> GetAssignProcessesByUserIdAsync(object userId);
    }
}
