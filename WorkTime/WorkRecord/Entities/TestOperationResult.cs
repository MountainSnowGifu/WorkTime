using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

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
            DifferenceReason = new DifferenceReason(differenceReason);
            IsWaitingTime = isWaitingTime;
            WaitingTimeReason = new WaitingTimeReason(waitingTimeReason);
        }

        public IOperation Operation { get; }
        public IWorkRecord WorkRecord { get; }
        public IOperatingUser OperatingUser { get; }
        public bool IsCompleted { get; }
        public bool IsLocalSaved { get; set; }
        public bool IsRemoteSaved { get; }
        public DifferenceReason DifferenceReason { get; }
        public bool IsWaitingTime { get; }
        public WaitingTimeReason WaitingTimeReason { get; }
    }
}
