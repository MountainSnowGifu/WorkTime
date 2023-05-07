using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkTime.Repository;
using WorkTime.SQLite;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;

namespace WorkTime.ViewModels
{
    public class WorkViewModel : BindableBase
    {
        private IOperationsRepository _operationsRepository;
        private OperationService _operationService;
        private IOperatingUser _operatingUser;

        public WorkViewModel()
        {
            _operationsRepository = new OperationsSQLite();
            _operatingUser = new TestUser("akira", "test", "test");
            _operationService = new OperationService(_operationsRepository.GetOperations(), _operatingUser);

            StartCommand.Subscribe(_ => StartCommandExecute());
            NextCommand.Subscribe(_ => NextCommandExecute());
            WaitCommand.Subscribe(_ => WaitCommandExecute());
            WaitEndCommand.Subscribe(_ => WaitEndCommandExecute());

            SelectedOperation.Subscribe(_ => SelectedOperationChangeExecute());

        }

        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Title");

        public ReactiveProperty<ObservableCollection<IOperation>> Operations { get; private set; }
        = new ReactiveProperty<ObservableCollection<IOperation>>();

        public ReactiveProperty<IOperation> SelectedOperation { get; private set; }
        = new ReactiveProperty<IOperation>();

        public ReactiveProperty<ObservableCollection<IOperationResult>> OperationResults { get; private set; }
        = new ReactiveProperty<ObservableCollection<IOperationResult>>();

        public ReactiveProperty<string> ProgressStatus { get; private set; } = new ReactiveProperty<string>("ProgressStatus");

        public ReactiveCommand StartCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand NextCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand WaitCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand WaitEndCommand { get; private set; } = new ReactiveCommand();

        private void StartCommandExecute()
        {
            Operations.Value = new ObservableCollection<IOperation>(_operationService.Operations);
            SelectedOperation.Value = _operationService.SelectedOperation;
        }

        private void NextCommandExecute()
        {
            if (SelectedOperation.Value == null)
            {
                return;
            }

            _operationService.OnCompletedNext();
            SelectedOperation.Value = _operationService.SelectedOperation;
            ProgressStatus.Value = _operationService.ProgressStatus;
        }

        private void WaitCommandExecute()
        {
            _operationService.WaitingStart();
        }

        private void WaitEndCommandExecute()
        {
            _operationService.WaitingEnd("test");
        }

        private void SelectedOperationChangeExecute()
        {
            if (_operationService.OperationResult.Count == 0)
            {
                return;
            }

            var unsavedOperationResults = _operationService.OperationResult.Where(x => x.IsLocalSaved == false).ToList();
            _operationsRepository.SaveOperationResults(unsavedOperationResults);
            foreach (var val in unsavedOperationResults)
            {
                val.IsLocalSaved = true;
            }
        }
    }
}
