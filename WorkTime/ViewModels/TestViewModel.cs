using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Transactions;
using WorkTime.Repository;
using WorkTime.SQLite;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;

namespace WorkTime.ViewModels
{
    public class TestViewModel : BindableBase
    {
        private IOperationsRepository _operationsRepository;
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Title");
        public ReactiveProperty<ObservableCollection<IOperation>> Operations { get; private set; }
       = new ReactiveProperty<ObservableCollection<IOperation>>();

        public ReactiveCommand TestCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand2 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand3 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand4 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand5 { get; private set; } = new ReactiveCommand();

        public TestViewModel()
        {
            _operationsRepository = new OperationsSQLite();
            TestCommand.Subscribe(_ => TestCommandExecute());
            TestCommand2.Subscribe(_ => Test2CommandExecute());
            TestCommand3.Subscribe(_ => Test3CommandExecute());
            TestCommand4.Subscribe(_ => Test4CommandExecute());
            TestCommand5.Subscribe(_ => Test5CommandExecute());
        }

        private void TestCommandExecute()
        {
            var sql = "INSERT INTO denco(no, name, type, attribute, maxap, maxhp, skill) VALUES(@no,12,13,14,15,16,17)";
            var parameters = new List<SQLiteParameter>();
            parameters.Add(new SQLiteParameter("@no", 200));

            var parameters2 = new List<SQLiteParameter>();
            parameters2.Add(new SQLiteParameter("@no", 300));

            using (var scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(5)))
            {
                SQLiteHelper.Execute(sql, parameters.ToArray());
                SQLiteHelper.Execute(sql, parameters2.ToArray());
                scope.Complete();
                


            }
        }

        private void Test2CommandExecute()
        {
            var sql = "Select name from denco";
            var parameters = new List<SQLiteParameter>();

            var list = new List<TestUser>();
            SQLiteHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                list.Add(new TestUser(
                                                                                 Convert.ToString(reader["name"]),
                                                                                 Environment.UserName,
                                                                                 Environment.MachineName));
                                            });
        }

        private void Test3CommandExecute()
        {
            var parameters = new List<SQLiteParameter>();
            var sql = @"
 DROP TABLE OPERATIONS;
";

            SQLiteHelper.Execute(sql, parameters.ToArray());

            sql = @"
CREATE TABLE IF NOT EXISTS
    OPERATIONS
(
    operation_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
    ,work_content_id TEXT NOT NULL
    ,work_content TEXT NOT NULL
    ,standard_work_time_seconds INTEGER
    ,target_work_time_seconds INTEGER
    ,contract TEXT
    ,work_order TEXT
    ,seg TEXT
    ,stage TEXT
    ,sfx TEXT
    ,section TEXT 
    ,worker_name TEXT 
)
";
            SQLiteHelper.Execute(sql, parameters.ToArray());
        }

        private void Test4CommandExecute()
        {
            //Operations.Value = new ObservableCollection<IOperation>(_operationsRepository.GetOperations());
            Operations.Value = new ObservableCollection<IOperation>(TestOperationService.GetOperations());
            _operationsRepository.SaveOperations(Operations.Value.ToList());
        }

        private void Test5CommandExecute()
        {
            var parameters = new List<SQLiteParameter>();
            var sql = @"
 DROP TABLE OPERATION_RESULTS;
";

            SQLiteHelper.Execute(sql, parameters.ToArray());

            sql = @"
CREATE TABLE IF NOT EXISTS
    OPERATION_RESULTS
(
    operation_result_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
    ,operation_id TEXT NOT NULL
    ,work_content_id TEXT NOT NULL
    ,work_content TEXT NOT NULL
    ,standard_work_time_seconds INTEGER
    ,target_work_time_seconds INTEGER
    ,contract TEXT
    ,work_order TEXT
    ,seg TEXT
    ,stage TEXT
    ,sfx TEXT
    ,section TEXT 
    ,worker_name TEXT 
    ,result_worker_name TEXT 
    ,result_user_name TEXT 
    ,result_machine_name TEXT 
    ,work_start_datetime DATETIME 
    ,work_end_datetime DATETIME 
    ,work_time_seconds INTEGER 
    ,is_completed INTEGER 
    ,is_remote_saved INTEGER 
    ,difference_reason TEXT 
    ,is_waiting_time INTEGER 
    ,waiting_time_reason TEXT 
)
";
            SQLiteHelper.Execute(sql, parameters.ToArray());
        }
    }
}
