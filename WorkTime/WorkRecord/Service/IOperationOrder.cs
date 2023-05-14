using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperationOrder : INotifyPropertyChanged
    {
        OperationOrderId RemoteOperationOrderId { get; }
        Contract Contract { get; }
        WorkOrder WorkOrder { get; }
        WorkerName WorkerName { get; }
        AffiliationCode AffiliationCode { get; }
        ScheduledWorkDate ScheduledWorkDate { get; }
        bool IsResponsibleCalling { get; set; }
        ResponsibleCallReason ResponsibleCallReason { get; set; }
        bool IsWaiting { get; set; }
        WaitingTimeReason WaitingTimeReason { get; set; }
        bool IsGroupWorking { get; set; }
        GroupWorkingOrder GroupWorkingOrder { get; set; }
        List<IOperationOrderDetail> OperationOrderDetails { get; }
        bool IsWorking { get; set; }
        bool IsDone { get; set; }
    }
}
