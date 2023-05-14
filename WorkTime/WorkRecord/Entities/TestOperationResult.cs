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
        public TestOperationResult(IOperationOrder operation, IOperationOrderDetail operationOrderDetail, IWorkRecord workRecord, IOperatingUser operatingUser, bool isCompleted, DifferenceReason differenceReason, bool isWaitingTime, WaitingTimeReason waitingTimeReason)
        {
            Operation = operation;
            OperationOrderDetail= operationOrderDetail;
            WorkRecord = workRecord;
            OperatingUser = operatingUser;
            IsCompleted = isCompleted;
            IsLocalSaved = false;
            IsRemoteSaved = false;
            DifferenceReason = differenceReason;
            IsWaitingTime = isWaitingTime;
            WaitingTimeReason = waitingTimeReason;
        }

        public IOperationOrder Operation { get; }
        public IOperationOrderDetail OperationOrderDetail { get; }
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
