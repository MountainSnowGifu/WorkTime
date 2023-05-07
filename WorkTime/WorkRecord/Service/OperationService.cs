using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Entities;

namespace WorkTime.WorkRecord.Service
{
    public sealed class OperationService
    {
        private DateTime _workStartDateTime;
        private DateTime _workEndDateTime;
        private DateTime _waitingStartDateTime;
        private DateTime _waitingEndDateTime;
        private IOperatingUser _operatingUser;

        private List<IOperation> _operations;
        private List<IOperationResult> _operationResults;
        private IOperation _selectedOperation;
        private string _progressStatus;
        private string _differenceReason;
        private bool _isWaiting;

        public OperationService(List<IOperation> operations, IOperatingUser operatingUser)
        {
            _operations = operations;
            _operatingUser = operatingUser;
            _selectedOperation = _operations.FirstOrDefault();
            _operationResults = new List<IOperationResult>();
            _differenceReason= string.Empty;
            _isWaiting = false;
            ProgressStatusUpdate();
            OperationStart();
        }

        public List<IOperation> Operations => _operations;
        public IOperation SelectedOperation => _selectedOperation;
        public List<IOperationResult> OperationResult => _operationResults;
        public string ProgressStatus => _progressStatus;

        private void ProgressStatusUpdate()
        {
            var operationsCount = _operations.Count;
            var operationsDoneCount = _operations.Where(x=>x.IsDone).Count();
            _progressStatus = operationsDoneCount + "/" + operationsCount;
        }

        private void OperationStart()
        {
            if (SelectedOperation == null)
            {
                return;
            }

            _differenceReason = string.Empty;
            _workStartDateTime = DateTime.Now;
            _workEndDateTime = DateTime.MaxValue;
        }

        private void OperationStop()
        {
            if (SelectedOperation == null)
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
            _selectedOperation.IsDone = false;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(SelectedOperation, workRecord, _operatingUser, true, _differenceReason,false,string.Empty));
        }

        public void WaitingEnd(string waitingReason)
        {
            if (!_isWaiting)
            {
                throw new Exception("作業中");
            }

            _waitingEndDateTime = DateTime.Now;
            var waitRecord = new TestWorkRecord(_waitingStartDateTime, _waitingEndDateTime);
            _operationResults.Add(new TestOperationResult(SelectedOperation, waitRecord, _operatingUser, true, _differenceReason, true, waitingReason));
            _isWaiting = false;
            OperationStart();
        }

        public void DifferenceReasonUpdate(string differenceReason)
        {
            _differenceReason= differenceReason;
        }

        public void OnCompletedNext()
        {
            if (_isWaiting)
            {
                throw new Exception("手待ち中");
            }

            OperationStop();
            _selectedOperation.IsDone = true;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(SelectedOperation, workRecord, _operatingUser, true, _differenceReason,false, string.Empty));
            _selectedOperation = Operations.SkipWhile(x => x != SelectedOperation).Skip(1).FirstOrDefault();
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
            _selectedOperation.IsDone = false;
            var workRecord = new TestWorkRecord(_workStartDateTime, _workEndDateTime);
            _operationResults.Add(new TestOperationResult(SelectedOperation, workRecord, _operatingUser, false, _differenceReason,false, string.Empty));
            _selectedOperation = Operations.SkipWhile(x => x != SelectedOperation).Skip(1).FirstOrDefault();
            ProgressStatusUpdate();
            OperationStart();
        }

    }
}
