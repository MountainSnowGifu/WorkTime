using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperationOrderDetail : INotifyPropertyChanged
    {
        OperationOrderId RemotoOperationOrderId { get; }
        OperationOrderDetailId RemotoOperationOrderDetailId { get; }
        WorkContentId WorkContentId { get; }
        WorkContent WorkContent { get; }
        SEG SEG { get; }
        Stage Stage { get; }
        SFX SFX { get; }
        Section Section { get; }
        TimeSpan StandardWorkTimeSeconds { get; }
        TimeSpan TargetWorkTimeSeconds { get; }
        bool IsDone { get; set; }
    }
}
