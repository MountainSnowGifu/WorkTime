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
        WorkContentReadAloud WorkContentReadAloud { get; }//読み上げ
        ImportantPoints ImportantPoints { get; }
        ImportantPointsImage ImportantPointsImage { get; }
        SEG SEG { get; }
        Stage Stage { get; }
        SFX SFX { get; }
        Section Section { get; }
        SqkIndex SqkIndex { get; }
        GenUnit GenUnit { get; }
        TimeSpan StandardWorkTimeSeconds { get; }
        TimeSpan TargetWorkTimeSeconds { get; }
        bool IsDone { get; set; }
        bool CanSkip { get; }
        bool IsSkip { get; set; }
    }
}
