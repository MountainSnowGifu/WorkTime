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
    public sealed class TestOperationOrder : IOperationOrder

    {
        public TestOperationOrder(
                                                int remoteOperationOrderId,
                                                string contract,
                                                string workOrder,
                                                string workerName,
                                                List<IOperationOrderDetail> operationOrderDetails,
                                                int affiliationCode,
                                                DateTime scheduledWorkDate,
                                                bool isResponsibleCalling,
                                                string responsibleCallReason,
                                                bool isWaiting,
                                                string waitingTimeReason,
                                                bool isGroupWorking,
                                                string groupWorkingOrder,
                                                bool isWorking,
                                                bool isDone)
        {
            RemoteOperationOrderId = new OperationOrderId(remoteOperationOrderId);
            Contract = new Contract(contract);
            WorkOrder = new WorkOrder(workOrder);
            WorkerName = new WorkerName(workerName);
            OperationOrderDetails = operationOrderDetails;
            AffiliationCode = new AffiliationCode(affiliationCode);
            ScheduledWorkDate = new ScheduledWorkDate(scheduledWorkDate);
            IsResponsibleCalling= isResponsibleCalling;
            ResponsibleCallReason = new ResponsibleCallReason(responsibleCallReason);
            IsWaiting = isWaiting;
            WaitingTimeReason = new WaitingTimeReason(waitingTimeReason);
            IsGroupWorking = isGroupWorking;
            GroupWorkingOrder = new GroupWorkingOrder(groupWorkingOrder);
            IsWorking = isWorking;
            IsDone = isDone;
        }

        public OperationOrderId RemoteOperationOrderId { get; }
        public Contract Contract { get; }
        public WorkOrder WorkOrder { get; }
        public WorkerName WorkerName { get; }
        public AffiliationCode AffiliationCode { get; }
        public ScheduledWorkDate ScheduledWorkDate { get; }
        public bool IsResponsibleCalling { get; set; }
        public ResponsibleCallReason ResponsibleCallReason { get; set; }
        public bool IsWaiting { get; set; }
        public WaitingTimeReason WaitingTimeReason { get; set; }
        public bool IsGroupWorking { get; set; }
        public GroupWorkingOrder GroupWorkingOrder { get; set; }
        public List<IOperationOrderDetail> OperationOrderDetails { get; }
        public bool IsWorking { get; set; }

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
