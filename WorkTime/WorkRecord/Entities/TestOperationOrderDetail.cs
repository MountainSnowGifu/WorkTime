using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestOperationOrderDetail : IOperationOrderDetail
    {
        public TestOperationOrderDetail(
                                                       int remotoOperationOrderDetailId,
                                                       int remotoOperationOrderId,
                                                       string workContentId,
                                                       string workContent,
                                                       string seg,
                                                       string stage,
                                                       string sfx,
                                                       string section,
                                                       string importantPoints,
                                                       string importantPointsImage,
                                                       string sqkIndex,
                                                       string genUnit,
                                                       TimeSpan standardWorkTimeSeconds,
                                                       TimeSpan targetWorkTimeSeconds,
                                                       bool isDone,
                                                       bool canSkip,
                                                       bool isSkip)
        {
            RemotoOperationOrderDetailId = new OperationOrderDetailId(remotoOperationOrderDetailId);
            RemotoOperationOrderId = new OperationOrderId(remotoOperationOrderId);
            WorkContentId = new WorkContentId(workContentId);
            WorkContent = new WorkContent(workContent);
            SEG = new SEG(seg);
            Stage = new Stage(stage);
            SFX = new SFX(sfx);
            Section = new Section(section);
            ImportantPoints = new ImportantPoints(importantPoints);
            ImportantPointsImage = new ImportantPointsImage(importantPointsImage);
            SqkIndex = new SqkIndex(sqkIndex);
            GenUnit = new GenUnit(genUnit);
            StandardWorkTimeSeconds = standardWorkTimeSeconds;
            TargetWorkTimeSeconds = targetWorkTimeSeconds;
            IsDone = isDone;
            CanSkip = canSkip;
            IsSkip = isSkip;
        }

        public OperationOrderId RemotoOperationOrderId { get; }
        public OperationOrderDetailId RemotoOperationOrderDetailId { get; }
        public WorkContentId WorkContentId { get; }
        public WorkContent WorkContent { get; }
        public SEG SEG { get; }
        public Stage Stage { get; }
        public SFX SFX { get; }
        public Section Section { get; }
        public ImportantPoints ImportantPoints { get; }
        public ImportantPointsImage ImportantPointsImage { get; }
        public SqkIndex SqkIndex { get; }
        public GenUnit GenUnit { get; }

        public TimeSpan StandardWorkTimeSeconds { get; }
        public TimeSpan TargetWorkTimeSeconds { get; }

        public bool CanSkip { get; }
        public bool IsSkip { get; set; }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
