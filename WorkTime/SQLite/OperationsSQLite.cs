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
        public List<IOperationOrder> GetOperationOrders()
        {
            var sql = @"
select
    OPERATIONS.[operation_id],
    OPERATIONS.[operation_order_id],
    OPERATIONS.[work_content_id],
    OPERATIONS.[work_content],
    OPERATIONS.[standard_work_time_seconds],
    OPERATIONS.[target_work_time_seconds],
    OPERATIONS.[contract],
    OPERATIONS.[work_order],
    OPERATIONS.[seg],
    OPERATIONS.[stage],
    OPERATIONS.[sfx],
    OPERATIONS.[section],
    OPERATIONS.[worker_name]
from
    OPERATIONS
;
";
            var parameters = new List<SQLiteParameter>();

            var list = new List<IOperationOrder>();
            SQLiteHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                //list.Add(new TestOperation(Convert.ToInt32(reader["operation_order_id"]),
                                                //                                        Convert.ToString(reader["contract"]),
                                                //                                        Convert.ToString(reader["work_order"]),
                                                //                                        Convert.ToString(reader["seg"]),
                                                //                                        Convert.ToString(reader["stage"]),
                                                //                                        Convert.ToString(reader["sfx"]),
                                                //                                        Convert.ToString(reader["section"]),
                                                //                                        Convert.ToString(reader["worker_name"]),
                                                //                                        null,
                                                //                                        1,
                                                //                                        false));
                                            });

            return list;
        }

        public void SaveOperationOrders(IOperationOrder operationOrder)
        {
            var sql = @"
insert into
    LOCAL_OPERATION_ORDERS (
        remote_operation_order_id,
        contract,
        work_order,
        worker_name,
        affiliation_code,
        is_done
    )
values
    (
        @remote_operation_order_id,
        @contract,
        @work_order,
        @worker_name,
        @affiliation_code,
        @is_done
    ) on conflict(remote_operation_order_id) do
update
set
    remote_operation_order_id = @remote_operation_order_id,
    contract = @contract,
    work_order = @work_order,
    worker_name = @worker_name,
    affiliation_code = @affiliation_code,
    is_done = @is_done
;";

            var parameters = new List<SQLiteParameter>();
            parameters.Clear();
            parameters.Add(new SQLiteParameter("@remote_operation_order_id", operationOrder.RemoteOperationOrderId.Value));
            parameters.Add(new SQLiteParameter("@contract", operationOrder.Contract.Value));
            parameters.Add(new SQLiteParameter("@work_order", operationOrder.WorkOrder.Value));
            parameters.Add(new SQLiteParameter("@worker_name", operationOrder.WorkerName.Value));
            parameters.Add(new SQLiteParameter("@affiliation_code", operationOrder.AffiliationCode.Value));
            parameters.Add(new SQLiteParameter("@is_done", operationOrder.IsDone));

            SQLiteHelper.Execute(sql, parameters.ToArray());

            SaveOperationOrderDetails(operationOrder.OperationOrderDetails);
        }

        private void SaveOperationOrderDetails(List<IOperationOrderDetail> operationOrderDetails)
        {
            var sql = @"
insert into
    LOCAL_OPERATION_ORDER_DETAILS (
        remote_operation_order_detail_id,
        remote_operation_order_id,
        work_content_id,
        work_content,
        seg,
        stage,
        sfx,
        section,
        standard_worktime_seconds,
        target_worktime_seconds,
        is_done
    )
values
    (
        @remote_operation_order_detail_id,
        @remote_operation_order_id,
        @work_content_id,
        @work_content,
        @seg,
        @stage,
        @sfx,
        @section,
        @standard_worktime_seconds,
        @target_worktime_seconds,
        @is_done
    ) on conflict(remote_operation_order_detail_id) do
update
set
    remote_operation_order_id = @remote_operation_order_id,
    work_content_id = @work_content_id,
    work_content = @work_content,
    seg = @seg,
    stage = @stage,
    sfx = @sfx,
    section = @section,
    standard_worktime_seconds = @standard_worktime_seconds,
    target_worktime_seconds = @target_worktime_seconds,
    is_done = @is_done
;";

            var parameters = new List<SQLiteParameter>();

            foreach (var val in operationOrderDetails)
            {
                parameters.Clear();
                parameters.Add(new SQLiteParameter("@remote_operation_order_detail_id", val.RemotoOperationOrderDetailId.Value));
                parameters.Add(new SQLiteParameter("@remote_operation_order_id", val.RemotoOperationOrderId.Value));
                parameters.Add(new SQLiteParameter("@work_content_id", val.WorkContentId.Value));
                parameters.Add(new SQLiteParameter("@work_content", val.WorkContent.Value));
                parameters.Add(new SQLiteParameter("@seg", val.SEG.Value));
                parameters.Add(new SQLiteParameter("@stage", val.Stage.Value));
                parameters.Add(new SQLiteParameter("@sfx", val.SFX.Value));
                parameters.Add(new SQLiteParameter("@section", val.Section.Value));
                parameters.Add(new SQLiteParameter("@standard_worktime_seconds", val.StandardWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@target_worktime_seconds", val.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@is_done", val.IsDone));
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
        operation_order_id,
        operation_order_detail_id,
        work_content_id,
        work_content,
        standard_work_time_seconds,
        target_work_time_seconds,
        contract,
        work_order,
        seg,
        stage,
        sfx,
        section,
        worker_name,
        result_worker_name,
        result_user_name,
        result_machine_name,
        work_start_datetime,
        work_end_datetime,
        work_time_seconds,
        is_completed,
        is_remote_saved,
        difference_reason,
        is_waiting_time,
        waiting_time_reason
    )
values
    (
        @operation_order_id,
        @operation_order_detail_id,
        @work_content_id,
        @work_content,
        @standard_work_time_seconds,
        @target_work_time_seconds,
        @contract,
        @work_order,
        @seg,
        @stage,
        @sfx,
        @section,
        @worker_name,
        @result_worker_name,
        @result_user_name,
        @result_machine_name,
        @work_start_datetime,
        @work_end_datetime,
        @work_time_seconds,
        @is_completed,
        @is_remote_saved,
        @difference_reason,
        @is_waiting_time,
        @waiting_time_reason
    );
";
            var parameters = new List<SQLiteParameter>();
            foreach (var result in operationResults)
            {
                parameters.Clear();
                parameters.Add(new SQLiteParameter("@operation_order_id", result.Operation.RemoteOperationOrderId.Value));
                parameters.Add(new SQLiteParameter("@operation_order_detail_id", result.OperationOrderDetail.RemotoOperationOrderDetailId.Value));
                parameters.Add(new SQLiteParameter("@work_content_id", result.OperationOrderDetail.WorkContentId.Value));
                parameters.Add(new SQLiteParameter("@work_content", result.OperationOrderDetail.WorkContent.Value));
                parameters.Add(new SQLiteParameter("@standard_work_time_seconds", result.OperationOrderDetail.StandardWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@target_work_time_seconds", result.OperationOrderDetail.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SQLiteParameter("@contract", result.Operation.Contract.Value));
                parameters.Add(new SQLiteParameter("@work_order", result.Operation.WorkOrder.Value));
                parameters.Add(new SQLiteParameter("@seg", result.OperationOrderDetail.SEG.Value));
                parameters.Add(new SQLiteParameter("@stage", result.OperationOrderDetail.Stage.Value));
                parameters.Add(new SQLiteParameter("@sfx", result.OperationOrderDetail.SFX.Value));
                parameters.Add(new SQLiteParameter("@section", result.OperationOrderDetail.Section.Value));
                parameters.Add(new SQLiteParameter("@worker_name", result.Operation.WorkerName.Value));

                parameters.Add(new SQLiteParameter("@result_worker_name", result.OperatingUser.ResultWorkerName.Value));
                parameters.Add(new SQLiteParameter("@result_user_name", result.OperatingUser.ResultUserName.Value));
                parameters.Add(new SQLiteParameter("@result_machine_name", result.OperatingUser.ResultMachineName.Value));
                parameters.Add(new SQLiteParameter("@work_start_datetime", result.WorkRecord.WorkStartDateTime.Value));
                parameters.Add(new SQLiteParameter("@work_end_datetime", result.WorkRecord.WorkEndDateTime.Value));
                parameters.Add(new SQLiteParameter("@work_time_seconds", result.WorkRecord.WorkTimeSeconds.TotalSeconds));
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
