using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperationResult
    {
        IOperation Operation { get; set; }
        IWorkRecord WorkRecord { get; set; }
        IOperatingUser OperatingUser { get; set; }
        bool IsCompleted { get; set; }
        bool IsLocalSaved { get; set; }
        bool IsRemoteSaved { get; set; }
        string DifferenceReason { get; set; }
        bool IsWaitingTime { get; set; }
        string WaitingTimeReason { get; set; }
    }
}
