using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        bool IsDone { get; set; }
        List<IOperationOrderDetail> OperationOrderDetails { get; }
    }
}
