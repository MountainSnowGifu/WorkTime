using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperationResult
    {
        IOperation Operation { get; }
        IWorkRecord WorkRecord { get; }
        IOperatingUser OperatingUser { get; }
        bool IsCompleted { get; }
        bool IsLocalSaved { get; set; }
        bool IsRemoteSaved { get; }
        DifferenceReason DifferenceReason { get; }
        bool IsWaitingTime { get; }
        WaitingTimeReason WaitingTimeReason { get; }
    }
}
