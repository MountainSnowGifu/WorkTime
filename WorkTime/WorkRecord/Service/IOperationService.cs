using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperationService
    {
        IOperationOrder OperationOrder { get; }
        List<IOperationOrderDetail> OperationOrderDetails { get; }
        IOperationOrderDetail SelectedOperationOrderDetail { get; }
        List<IOperationResult> OperationResult { get; }
        string ProgressStatus { get; }

        void WaitingStart();
        void WaitingEnd(WaitingTimeReason waitingTimeReason);
        void DifferenceReasonUpdate(DifferenceReason differenceReason);
        void OnCompletedNext();
        void InCompletedNext();
    }
}
