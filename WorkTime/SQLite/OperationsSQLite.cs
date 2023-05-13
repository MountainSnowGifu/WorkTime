using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.Repository;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;

namespace WorkTime.SQLite
{
    internal class OperationsSQLite : IOperationsRepository
    {
        public List<IOperation> GetOperations()
        {
            var sql = @"
select
        OPERATIONS.[operation_id]
        ,OPERATIONS.[work_content_id]
        ,OPERATIONS.[work_content]
        ,OPERATIONS.[standard_work_time_seconds]
        ,OPERATIONS.[target_work_time_seconds]
        ,OPERATIONS.[contract]
        ,OPERATIONS.[work_order]
        ,OPERATIONS.[seg]
        ,OPERATIONS.[stage]
        ,OPERATIONS.[sfx]
        ,OPERATIONS.[section]
        ,OPERATIONS.[worker_name]

from OPERATIONS
;
";
            var parameters = new List<SQLiteParameter>();

            var list = new List<IOperation>();
            SQLiteHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                list.Add(new TestOperation(Convert.ToInt32(reader["operation_id"]),
                                                                                                            Convert.ToString(reader["work_content_id"]),
                                                                                                            Convert.ToString(reader["work_content"]),
                                                                                                            new TimeSpan(0, 0, Convert.ToInt32(reader["standard_work_time_seconds"])),
                                                                                                            new TimeSpan(0, 0, Convert.ToInt32(reader["target_work_time_seconds"])),
                                                                                                            Convert.ToString(reader["contract"]),
                                                                                                            Convert.ToString(reader["work_order"]),
                                                                                                            Convert.ToString(reader["seg"]),
                                                                                                            Convert.ToString(reader["stage"]),
                                                                                                            Convert.ToString(reader["sfx"]),
                                                                                                            Convert.ToString(reader["section"]),
                                                                                                            Convert.ToString(reader["worker_name"])));
                                            });

            return list;
        }

        public void SaveOperations(List<IOperation> operations)
        {
            var sql = @"
insert into 
    OPERATIONS (
        operation_id
        ,work_content_id
        ,work_content
        ,standard_work_time_seconds
        ,target_work_time_seconds
        ,contract
        ,work_order
        ,seg
        ,stage
        ,sfx
        ,section
        ,worker_name
    )
values (
        @operation_id
        ,@work_content_id
        ,@work_content
        ,@standard_work_time_seconds
        ,@target_work_time_seconds
        ,@contract
        ,@work_order
        ,@seg
        ,@stage
        ,@sfx
        ,@section
        ,@worker_name
    )
on conflict(operation_id)
do update
    set
        work_content_id = @work_content_id
        ,work_content = @work_content
        ,standard_work_time_seconds = @standard_work_time_seconds
        ,target_work_time_seconds = @target_work_time_seconds
        ,contract = @contract
        ,work_order = @work_order
        ,seg = @seg
        ,stage = @stage
        ,sfx = @sfx
        ,section = @section
        ,worker_name = @worker_name

;
";
            var parameters = new List<SQLiteParameter>();
            foreach(var operation in operations)
            {
                parameters.Clear();
                parameters.Add(new SQLiteParameter("@operation_id", operation.OperationId.Value));
                parameters.Add(new SQLiteParameter("@work_content_id", operation.WorkContentId.Value));
                parameters.Add(new SQLiteParameter("@work_content", operation.WorkContent.Value));
                parameters.Add(new SQLiteParameter("@standard_work_time_seconds", operation.StandardWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@target_work_time_seconds", operation.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@contract", operation.Contract.Value));
                parameters.Add(new SQLiteParameter("@work_order", operation.WorkOrder.Value));
                parameters.Add(new SQLiteParameter("@seg", operation.SEG.Value));
                parameters.Add(new SQLiteParameter("@stage", operation.Stage.Value));
                parameters.Add(new SQLiteParameter("@sfx", operation.SFX.Value));
                parameters.Add(new SQLiteParameter("@section", operation.Section.Value));
                parameters.Add(new SQLiteParameter("@worker_name", operation.WorkerName.Value));
                SQLiteHelper.Execute(sql, parameters.ToArray());
            }

        }

        public List<IOperationResult> GetOperationResults()
        {
            throw new NotImplementedException();
        }

        public void SaveOperationResults(List<IOperationResult> operationResults)
        {
            var sql = @"
insert into 
    OPERATION_RESULTS (
         operation_id
        ,work_content_id
        ,work_content
        ,standard_work_time_seconds
        ,target_work_time_seconds
        ,contract
        ,work_order
        ,seg
        ,stage
        ,sfx
        ,section
        ,worker_name

        ,result_worker_name
        ,result_user_name
        ,result_machine_name
        ,work_start_datetime
        ,work_end_datetime
        ,work_time_seconds
        ,is_completed
        ,is_remote_saved
        ,difference_reason
        ,is_waiting_time
        ,waiting_time_reason
    )
values
    (
         @operation_id
        ,@work_content_id
        ,@work_content
        ,@standard_work_time_seconds
        ,@target_work_time_seconds
        ,@contract
        ,@work_order
        ,@seg
        ,@stage
        ,@sfx
        ,@section
        ,@worker_name

        ,@result_worker_name
        ,@result_user_name
        ,@result_machine_name
        ,@work_start_datetime
        ,@work_end_datetime
        ,@work_time_seconds
        ,@is_completed
        ,@is_remote_saved
        ,@difference_reason
        ,@is_waiting_time
        ,@waiting_time_reason
    )
;
";
            var parameters = new List<SQLiteParameter>();
            foreach (var result in operationResults)
            {
                parameters.Clear();
                parameters.Add(new SQLiteParameter("@operation_id", result.Operation.OperationId.Value));
                parameters.Add(new SQLiteParameter("@work_content_id", result.Operation.WorkContentId.Value));
                parameters.Add(new SQLiteParameter("@work_content", result.Operation.WorkContent.Value));
                parameters.Add(new SQLiteParameter("@standard_work_time_seconds", result.Operation.StandardWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@target_work_time_seconds", result.Operation.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@contract", result.Operation.Contract.Value));
                parameters.Add(new SQLiteParameter("@work_order", result.Operation.WorkOrder.Value));
                parameters.Add(new SQLiteParameter("@seg", result.Operation.SEG.Value));
                parameters.Add(new SQLiteParameter("@stage", result.Operation.Stage.Value));
                parameters.Add(new SQLiteParameter("@sfx", result.Operation.SFX.Value));
                parameters.Add(new SQLiteParameter("@section", result.Operation.Section.Value));
                parameters.Add(new SQLiteParameter("@worker_name", result.Operation.WorkerName.Value));

                parameters.Add(new SQLiteParameter("@result_worker_name", result.OperatingUser.ResultWorkerName.Value));
                parameters.Add(new SQLiteParameter("@result_user_name", result.OperatingUser.ResultUserName.Value));
                parameters.Add(new SQLiteParameter("@result_machine_name", result.OperatingUser.ResultMachineName.Value));
                parameters.Add(new SQLiteParameter("@work_start_datetime", result.WorkRecord.WorkStartDateTime.Value));
                parameters.Add(new SQLiteParameter("@work_end_datetime", result.WorkRecord.WorkEndDateTime.Value));
                parameters.Add(new SQLiteParameter("@work_time_seconds", result.WorkRecord.WorkTimeSeconds));
                parameters.Add(new SQLiteParameter("@is_completed", result.IsCompleted));
                parameters.Add(new SQLiteParameter("@is_remote_saved", result.IsRemoteSaved));
                parameters.Add(new SQLiteParameter("@difference_reason", result.DifferenceReason.Value));
                parameters.Add(new SQLiteParameter("@is_waiting_time", result.IsWaitingTime));
                parameters.Add(new SQLiteParameter("@waiting_time_reason", result.WaitingTimeReason.Value));

                SQLiteHelper.Execute(sql, parameters.ToArray());
            }

        }
    }
}
