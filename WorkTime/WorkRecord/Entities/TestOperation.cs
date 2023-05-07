using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;

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
            OperationId = operationId;
            WorkContentId = workContentId;
            WorkContent = workContent;
            StandardWorkTimeSeconds = standardWorkTimeSeconds;
            TargetWorkTimeSeconds = targetWorkTimeSeconds;
            Contract = contract;
            WorkOrder = workOrder;
            SEG = seg;
            Stage = stage;
            SFX = sfx;
            Section = section;
            WorkerName = workerName;
            IsDone = false;
        }

        public int OperationId { get; set; }
        public string WorkContentId { get; set; }
        public string WorkContent { get; set; }
        public TimeSpan StandardWorkTimeSeconds { get; set; }
        public TimeSpan TargetWorkTimeSeconds { get; set; }
        public string Contract { get; set; }
        public string WorkOrder { get; set; }
        public string SEG { get; set; }
        public string Stage { get; set; }
        public string SFX { get; set; }
        public string Section { get; set; }
        public string WorkerName { get; set; }

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
