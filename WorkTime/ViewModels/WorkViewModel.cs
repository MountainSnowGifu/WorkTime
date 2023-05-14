using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkTime.Repository;
using WorkTime.SQLite;
using WorkTime.SQLServer;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.ViewModels
{
    public class WorkViewModel : BindableBase
    {
        private IOperationsRepository _operationsRepositoryLocal;
        private IOperationsRepository _operationsRepositoryRemote;
        private IOperationService _operationService;
        private IOperatingUser _operatingUser;

        public WorkViewModel()
        {
            _operationsRepositoryLocal = new OperationsSQLite();
            _operationsRepositoryRemote = new OperationsSQLServer();
            _operatingUser = new TestUser("akira", "test", "test");

            var operationOrders = _operationsRepositoryRemote.GetOperationOrders();
            _operationsRepositoryLocal.SaveOperationOrders(operationOrders[0]);

            SelectedOperationOrder.Value = _operationsRepositoryLocal.GetOperationOrders()[0];

            _operationService = new OperationService(SelectedOperationOrder.Value, _operatingUser);

            StartCommand.Subscribe(_ => StartCommandExecute());
            NextCommand.Subscribe(_ => NextCommandExecute());
            WaitCommand.Subscribe(_ => WaitCommandExecute());
            WaitEndCommand.Subscribe(_ => WaitEndCommandExecute());

            SelectedOperationOrderDetails.Subscribe(_ => SelectedOperationChangeExecute());
        }

        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Title");

        public ReactiveProperty<IOperationOrder> SelectedOperationOrder { get; private set; }
       = new ReactiveProperty<IOperationOrder>();

        public ReactiveProperty<ObservableCollection<IOperationOrderDetail>> OperationOrderDetails { get; private set; }
        = new ReactiveProperty<ObservableCollection<IOperationOrderDetail>>();

        public ReactiveProperty<IOperationOrderDetail> SelectedOperationOrderDetails { get; private set; }
        = new ReactiveProperty<IOperationOrderDetail>();

        public ReactiveProperty<ObservableCollection<IOperationResult>> OperationResults { get; private set; }
        = new ReactiveProperty<ObservableCollection<IOperationResult>>();

        public ReactiveProperty<string> ProgressStatus { get; private set; } = new ReactiveProperty<string>("ProgressStatus");

        public ReactiveCommand StartCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand NextCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand WaitCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand WaitEndCommand { get; private set; } = new ReactiveCommand();

        private void StartCommandExecute()
        {
            OperationOrderDetails.Value = new ObservableCollection<IOperationOrderDetail>(_operationService.OperationOrderDetails);
            SelectedOperationOrderDetails.Value = _operationService.SelectedOperationOrderDetail;
        }

        private void NextCommandExecute()
        {
            if (SelectedOperationOrderDetails.Value == null)
            {
                return;
            }

            _operationService.OnCompletedNext();
            SelectedOperationOrderDetails.Value = _operationService.SelectedOperationOrderDetail;
            ProgressStatus.Value = _operationService.ProgressStatus;
        }

        private void WaitCommandExecute()
        {
            _operationService.WaitingStart();
        }

        private void WaitEndCommandExecute()
        {
            _operationService.WaitingEnd(new WaitingTimeReason("test"));
        }

        private void SelectedOperationChangeExecute()
        {
            if (_operationService.OperationResult.Count == 0)
            {
                return;
            }

            var unsavedOperationResults = _operationService.OperationResult.Where(x => x.IsLocalSaved == false).ToList();
            _operationsRepositoryLocal.SaveOperationResults(unsavedOperationResults);
            //_operationsRepositoryRemote.SaveOperationResults(unsavedOperationResults);
            foreach (var val in unsavedOperationResults)
            {
                val.IsLocalSaved = true;
            }
        }
    }
}
