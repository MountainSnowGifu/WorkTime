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
                                                       TimeSpan standardWorkTimeSeconds,
                                                       TimeSpan targetWorkTimeSeconds,
                                                       bool isDone)
        {
            RemotoOperationOrderDetailId = new OperationOrderDetailId(remotoOperationOrderDetailId);
            RemotoOperationOrderId = new OperationOrderId(remotoOperationOrderId);
            WorkContentId = new WorkContentId(workContentId);
            WorkContent = new WorkContent(workContent);
            SEG = new SEG(seg);
            Stage = new Stage(stage);
            SFX = new SFX(sfx);
            Section = new Section(section);
            StandardWorkTimeSeconds = standardWorkTimeSeconds;
            TargetWorkTimeSeconds = targetWorkTimeSeconds;
            IsDone = isDone;
        }

        public OperationOrderId RemotoOperationOrderId { get; }
        public OperationOrderDetailId RemotoOperationOrderDetailId { get; }
        public WorkContentId WorkContentId { get; }
        public WorkContent WorkContent { get; }
        public SEG SEG { get; }
        public Stage Stage { get; }
        public SFX SFX { get; }
        public Section Section { get; }
        public TimeSpan StandardWorkTimeSeconds { get; }
        public TimeSpan TargetWorkTimeSeconds { get; }

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
