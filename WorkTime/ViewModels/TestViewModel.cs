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
using WorkTime.SQLServer;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;

namespace WorkTime.ViewModels
{
    public class TestViewModel : BindableBase
    {
        private IOperationsRepository _operationsRepositoryLocal;
        private IOperationsRepository _operationsRepositoryRemote;
        public ReactiveProperty<string> Title { get; private set; } = new ReactiveProperty<string>("Title");
        public ReactiveProperty<ObservableCollection<IOperationOrder>> OperationOrders { get; private set; }
       = new ReactiveProperty<ObservableCollection<IOperationOrder>>();

        public ReactiveCommand TestCommand { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand2 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand3 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand4 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand5 { get; private set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand6 { get; private set; } = new ReactiveCommand();

        public TestViewModel()
        {
            _operationsRepositoryLocal = new OperationsSQLite();
            _operationsRepositoryRemote = new OperationsSQLServer();
            TestCommand.Subscribe(_ => TestCommandExecute());
            TestCommand2.Subscribe(_ => Test2CommandExecute());
            TestCommand3.Subscribe(_ => Test3CommandExecute());
            TestCommand4.Subscribe(_ => Test4CommandExecute());
            TestCommand5.Subscribe(_ => Test5CommandExecute());
            TestCommand6.Subscribe(_ => Test6CommandExecute());
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
 DROP TABLE LOCAL_OPERATION_ORDERS;
";

            SQLiteHelper.Execute(sql, parameters.ToArray());

            sql = @"
CREATE TABLE IF NOT EXISTS
    LOCAL_OPERATION_ORDERS
(
    remote_operation_order_id INTEGER NOT NULL UNIQUE
    ,contract TEXT NOT NULL
    ,work_order TEXT NOT NULL
    ,worker_name TEXT
    ,affiliation_code TEXT
    ,is_done TEXT
)
";
            SQLiteHelper.Execute(sql, parameters.ToArray());
        }

        private void Test6CommandExecute()
        {
            var parameters = new List<SQLiteParameter>();
            var sql = @"
 DROP TABLE LOCAL_OPERATION_ORDER_DETAILS;
";

            SQLiteHelper.Execute(sql, parameters.ToArray());

            sql = @"
CREATE TABLE IF NOT EXISTS
    LOCAL_OPERATION_ORDER_DETAILS
(
    remote_operation_order_detail_id INTEGER NOT NULL UNIQUE
    ,remote_operation_order_id INTEGER NOT NULL
    ,work_content_id TEXT NOT NULL
    ,work_content TEXT NOT NULL
    ,seg TEXT
    ,stage TEXT
    ,sfx TEXT
    ,section TEXT
    ,standard_worktime_seconds TEXT
    ,target_worktime_seconds TEXT
    ,is_done INTEGER
)
";
            SQLiteHelper.Execute(sql, parameters.ToArray());
        }

        private void Test4CommandExecute()
        {
            //Operations.Value = new ObservableCollection<IOperation>(_operationsRepository.GetOperations());
            OperationOrders.Value = new ObservableCollection<IOperationOrder>(TestOperationService.GetOperations());
            _operationsRepositoryRemote.SaveOperationOrders(OperationOrders.Value[0]);
            //_operationsRepositoryLocal.SaveOperations(OperationOrders.Value.ToList());
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
    ,operation_order_id INTEGER NOT NULL
    ,operation_order_detail_id INTEGER NOT NULL
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
