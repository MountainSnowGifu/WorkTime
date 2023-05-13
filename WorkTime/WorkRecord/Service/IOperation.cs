using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperation : INotifyPropertyChanged
    {
        OperationId OperationId { get; }
        WorkContentId WorkContentId { get; }
        WorkContent WorkContent { get; }
        TimeSpan StandardWorkTimeSeconds { get; }
        TimeSpan TargetWorkTimeSeconds { get; }
        Contract Contract { get; }
        WorkOrder WorkOrder { get; }
        SEG SEG { get; }
        Stage Stage { get; }
        SFX SFX { get; }
        Section Section { get; }
        WorkerName WorkerName { get; }
        bool IsDone { get; set; }
    }
}
