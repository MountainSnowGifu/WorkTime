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
    public sealed class TestOperation : IOperation

    {
        public TestOperation(
                                        int operationId,
                                        string workContentId,
                                        string workContent,
                                        TimeSpan standardWorkTimeSeconds,
                                        TimeSpan targetWorkTimeSeconds,
                                        string contract,
                                        string workOrder,
                                        string seg,
                                        string stage,
                                        string sfx,
                                        string section,
                                        string workerName)
        {
            OperationId = new OperationId(operationId);
            WorkContentId = new WorkContentId(workContentId);
            WorkContent = new WorkContent(workContent);
            StandardWorkTimeSeconds = standardWorkTimeSeconds;
            TargetWorkTimeSeconds = targetWorkTimeSeconds;
            Contract = new Contract(contract);
            WorkOrder = new WorkOrder(workOrder);
            SEG = new SEG(seg);
            Stage = new Stage(stage);
            SFX = new SFX(sfx);
            Section = new Section(section);
            WorkerName = new WorkerName(workerName);
            IsDone = false;
        }

        public OperationId OperationId { get; }
        public WorkContentId WorkContentId { get; }
        public WorkContent WorkContent { get; }
        public TimeSpan StandardWorkTimeSeconds { get; }
        public TimeSpan TargetWorkTimeSeconds { get; }
        public Contract Contract { get; }
        public WorkOrder WorkOrder { get; }
        public SEG SEG { get; }
        public Stage Stage { get; }
        public SFX SFX { get; }
        public Section Section { get; }
        public WorkerName WorkerName { get; }

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
