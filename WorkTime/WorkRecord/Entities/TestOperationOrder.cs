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
                                        bool isDone)
        {
            RemoteOperationOrderId = new OperationOrderId(remoteOperationOrderId);
            Contract = new Contract(contract);
            WorkOrder = new WorkOrder(workOrder);
            WorkerName = new WorkerName(workerName);
            IsDone = isDone;
            OperationOrderDetails=operationOrderDetails;
            AffiliationCode= new AffiliationCode(affiliationCode);
        }

        public OperationOrderId RemoteOperationOrderId { get; }
        public Contract Contract { get; }
        public WorkOrder WorkOrder { get; }
        public WorkerName WorkerName { get; }
        public AffiliationCode AffiliationCode { get; }
        public List<IOperationOrderDetail> OperationOrderDetails { get; }

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
