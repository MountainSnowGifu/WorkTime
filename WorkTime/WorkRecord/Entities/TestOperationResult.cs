using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestOperationResult : IOperationResult
    {
        public TestOperationResult(IOperation operation, IWorkRecord workRecord, IOperatingUser operatingUser, bool isCompleted, string differenceReason, bool isWaitingTime, string waitingTimeReason)
        {
            Operation = operation;
            WorkRecord = workRecord;
            OperatingUser = operatingUser;
            IsCompleted = isCompleted;
            IsLocalSaved = false;
            IsRemoteSaved = false;
            DifferenceReason = differenceReason;
            IsWaitingTime = isWaitingTime;
            WaitingTimeReason = waitingTimeReason;
        }

        public IOperation Operation { get; set; }
        public IWorkRecord WorkRecord { get; set; }
        public IOperatingUser OperatingUser { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsLocalSaved { get; set; }
        public bool IsRemoteSaved { get; set; }
        public string DifferenceReason { get; set; }
        public bool IsWaitingTime { get; set; }
        public string WaitingTimeReason { get; set; }
    }
}
