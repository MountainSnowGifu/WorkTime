using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public sealed class OperationService : IOperationService
    {
        private DateTime _workStartDateTime;
        private DateTime _workEndDateTime;
        private DateTime _waitingStartDateTime;
        private DateTime _waitingEndDateTime;
        private IOperatingUser _operatingUser;

        private IOperationOrder _operationOrder;
        private List<IOperationOrderDetail> _operationOrderDetails;
        private List<IOperationResult> _operationResults;
        private IOperationOrderDetail _selectedOperationOrderDetail;
        private string _progressStatus;
        private DifferenceReason _differenceReason;
        private bool _isWaiting;

        public OperationService(IOperationOrder operationOrder, IOperatingUser operatingUser)
        {
            _operationOrder = operationOrder;
            _operationOrderDetails = operationOrder.OperationOrderDetails;
            _operatingUser = operatingUser;
            _selectedOperationOrderDetail = _operationOrderDetails.FirstOrDefault();
            _operationResults = new List<IOperationResult>();
            _differenceReason = new DifferenceReason(string.Empty);
            _isWaiting = false;
            ProgressStatusUpdate();
            OperationStart();
        }

        public IOperationOrder OperationOrder => _operationOrder;
        public List<IOperationOrderDetail> OperationOrderDetails => _operationOrderDetails;
        public IOperationOrderDetail SelectedOperationOrderDetail => _selectedOperationOrderDetail;
        public List<IOperationResult> OperationResult => _operationResults;
        public string ProgressStatus => _progressStatus;

        private void ProgressStatusUpdate()
        {
            var operationsCount = _operationOrderDetails.Count;
            var operationsDoneCount = _operationOrderDetails.Where(x => x.IsDone).Count();
            _progressStatus = operationsDoneCount + "/" + operationsCount;
        }

        private void OperationStart()
        {
            if (SelectedOperationOrderDetail == null)
            {
                return;
            }

            _differenceReason = new DifferenceReason(string.Empty);
            _workStartDateTime = DateTime.Now;
            _workEndDateTime = DateTime.MaxValue;
        }

        private void OperationStop()
        {
            if (SelectedOperationOrderDetail == null)
            {
                return;
            }

            _workEndDateTime = DateTime.Now;
        }

        public void WaitingStart()
        {
            if (_isWaiting)
            {
                throw new Exception("手待ち中");
            }

            _isWaiting = true;
            _waitingStartDateTime = DateTime.Now;
            _waitingEndDateTime = DateTime.MaxValue;

            OperationStop();
            SelectedOperationOrderDetail.IsDone = false;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(OperationOrder, SelectedOperationOrderDetail, workRecord, _operatingUser, true, _differenceReason, false, new WaitingTimeReason(string.Empty)));
        }

        public void WaitingEnd(WaitingTimeReason waitingTimeReason)
        {
            if (!_isWaiting)
            {
                throw new Exception("作業中");
            }

            _waitingEndDateTime = DateTime.Now;
            var waitRecord = new TestWorkRecord(_waitingStartDateTime, _waitingEndDateTime);
            _operationResults.Add(new TestOperationResult(OperationOrder, SelectedOperationOrderDetail, waitRecord, _operatingUser, true, _differenceReason, true, waitingTimeReason));
            _isWaiting = false;
            OperationStart();
        }

        public void DifferenceReasonUpdate(DifferenceReason differenceReason)
        {
            _differenceReason = differenceReason;
        }

        public void OnCompletedNext()
        {
            if (_isWaiting)
            {
                throw new Exception("手待ち中");
            }

            OperationStop();
            SelectedOperationOrderDetail.IsDone = true;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(OperationOrder, SelectedOperationOrderDetail, workRecord, _operatingUser, true, _differenceReason, false, new WaitingTimeReason(string.Empty)));
            _selectedOperationOrderDetail = _operationOrderDetails.SkipWhile(x => x != SelectedOperationOrderDetail).Skip(1).FirstOrDefault();
            ProgressStatusUpdate();
            OperationStart();
        }

        public void InCompletedNext()
        {
            if (_isWaiting)
            {
                throw new Exception("手待ち中");
            }

            OperationStop();
            SelectedOperationOrderDetail.IsDone = false;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(OperationOrder, SelectedOperationOrderDetail, workRecord, _operatingUser, false, _differenceReason, false, new WaitingTimeReason(string.Empty)));
            _selectedOperationOrderDetail = _operationOrderDetails.SkipWhile(x => x != SelectedOperationOrderDetail).Skip(1).FirstOrDefault();
            ProgressStatusUpdate();
            OperationStart();
        }
    }
}
