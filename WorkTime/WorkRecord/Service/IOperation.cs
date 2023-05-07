using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperation : INotifyPropertyChanged
    {
        int OperationId { get; set; }
        string WorkContentId { get; set; }
        string WorkContent { get; set; }
        TimeSpan StandardWorkTimeSeconds { get; set; }
        TimeSpan TargetWorkTimeSeconds { get; set; }
        string Contract { get; set; }
        string WorkOrder { get; set; }
        string SEG { get; set; }
        string Stage { get; set; }
        string SFX { get; set; }
        string Section { get; set; }
        string WorkerName { get; set; }
        bool IsDone { get; set; }
    }
}
