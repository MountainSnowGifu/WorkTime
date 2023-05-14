using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.Repository;
using WorkTime.SQLite;
using WorkTime.WorkRecord.Entities;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.SQLServer
{
    internal class OperationsSQLServer : IOperationsRepository
    {
        public List<IOperationResult> GetOperationResults()
        {
            throw new NotImplementedException();
        }

        public void SaveOperationResults(List<IOperationResult> operationResults)
        {
            var sql = @"
insert into
    [WorkTime].[dbo].[OPERATION_RESULTS] (
        operation_id,
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
        @operation_id,
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
            var parameters = new List<SqlParameter>();
            foreach (var result in operationResults)
            {
                parameters.Clear();
                //parameters.Add(new SqlParameter("@operation_id", result.Operation.OperationId.Value));
                //parameters.Add(new SqlParameter("@work_content_id", result.Operation.WorkContentId.Value));
                //parameters.Add(new SqlParameter("@work_content", result.Operation.WorkContent.Value));
                //parameters.Add(new SqlParameter("@standard_work_time_seconds", result.Operation.StandardWorkTimeSeconds.TotalSeconds));
                //parameters.Add(new SqlParameter("@target_work_time_seconds", result.Operation.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SqlParameter("@contract", result.Operation.Contract.Value));
                parameters.Add(new SqlParameter("@work_order", result.Operation.WorkOrder.Value));
                //parameters.Add(new SqlParameter("@seg", result.Operation.SEG.Value));
                //parameters.Add(new SqlParameter("@stage", result.Operation.Stage.Value));
                //parameters.Add(new SqlParameter("@sfx", result.Operation.SFX.Value));
                //parameters.Add(new SqlParameter("@section", result.Operation.Section.Value));
                parameters.Add(new SqlParameter("@worker_name", result.Operation.WorkerName.Value));

                parameters.Add(new SqlParameter("@result_worker_name", result.OperatingUser.ResultWorkerName.Value));
                parameters.Add(new SqlParameter("@result_user_name", result.OperatingUser.ResultUserName.Value));
                parameters.Add(new SqlParameter("@result_machine_name", result.OperatingUser.ResultMachineName.Value));
                parameters.Add(new SqlParameter("@work_start_datetime", result.WorkRecord.WorkStartDateTime.Value));
                parameters.Add(new SqlParameter("@work_end_datetime", result.WorkRecord.WorkEndDateTime.Value));
                parameters.Add(new SqlParameter("@work_time_seconds", result.WorkRecord.WorkTimeSeconds.TotalSeconds));
                parameters.Add(new SqlParameter("@is_completed", result.IsCompleted));
                parameters.Add(new SqlParameter("@is_remote_saved", result.IsRemoteSaved));
                parameters.Add(new SqlParameter("@difference_reason", result.DifferenceReason.Value));
                parameters.Add(new SqlParameter("@is_waiting_time", result.IsWaitingTime));
                parameters.Add(new SqlParameter("@waiting_time_reason", result.WaitingTimeReason.Value));

                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
        }

        public List<IOperationOrder> GetOperationOrders()
        {
            var sql = @"
select
    ORDERS.[remote_operation_order_id],
    ORDERS.[contract],
    ORDERS.[work_order],
    ORDERS.[worker_name],
    ORDERS.[affiliation_code],
    ORDERS.[is_done]
from
    [WorkTime].[dbo].[OPERATION_ORDERS] as ORDERS;
";
            var parameters = new List<SqlParameter>();
            int remoteOperationOrderId;
            var list = new List<IOperationOrder>();
            SQLServerHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                remoteOperationOrderId = Convert.ToInt32(reader["remote_operation_order_id"]);
                                                list.Add(new TestOperationOrder(remoteOperationOrderId,
                                                                                        Convert.ToString(reader["contract"]),
                                                                                        Convert.ToString(reader["work_order"]),
                                                                                        Convert.ToString(reader["worker_name"]),
                                                                                        GetOperationOrderDetails(remoteOperationOrderId),
                                                                                        Convert.ToInt32(reader["affiliation_code"]),
                                                                                        Convert.ToBoolean(reader["is_done"])));
                                            });

            return list;
        }

        private List<IOperationOrderDetail> GetOperationOrderDetails(int remoteOperationOrderId)
        {
            var sql = @"
select
    DETAILS.[remote_operation_order_detail_id],
    DETAILS.[remote_operation_order_id],
    DETAILS.[work_content_id],
    DETAILS.[work_content],
    DETAILS.[seg],
    DETAILS.[stage],
    DETAILS.[sfx],
    DETAILS.[section],
    DETAILS.[standard_worktime_seconds],
    DETAILS.[target_worktime_seconds],
    DETAILS.[is_done]
from
    [WorkTime].[dbo].[OPERATION_ORDER_DETAILS] as DETAILS
where 
    DETAILS.[remote_operation_order_id] = @remote_operation_order_id;
";


            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@remote_operation_order_id", remoteOperationOrderId));

            var list = new List<IOperationOrderDetail>();
            SQLServerHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                list.Add(new TestOperationOrderDetail(
                                                                                                        Convert.ToInt32(reader["remote_operation_order_detail_id"]),
                                                                                                        Convert.ToInt32(reader["remote_operation_order_id"]),
                                                                                                        Convert.ToString(reader["work_content_id"]),
                                                                                                        Convert.ToString(reader["work_content"]),
                                                                                                        Convert.ToString(reader["seg"]),
                                                                                                        Convert.ToString(reader["stage"]),
                                                                                                        Convert.ToString(reader["sfx"]),
                                                                                                        Convert.ToString(reader["section"]),
                                                                                                        new TimeSpan(0, 0, 0, Convert.ToInt32(reader["standard_worktime_seconds"])),
                                                                                                        new TimeSpan(0, 0, 0, Convert.ToInt32(reader["target_worktime_seconds"])),
                                                                                                        Convert.ToBoolean(reader["is_done"])));
                                            });

            return list;
        }

        public void SaveOperationOrders(IOperationOrder operationOrder)
        {
            var sql = @"
insert into
    [WorkTime].[dbo].[OPERATION_ORDERS] (
        contract,
        work_order,
        worker_name,
        affiliation_code,
        is_done
    )
values
    (
        @contract,
        @work_order,
        @worker_name,
        @affiliation_code,
        @is_done
    )
Select
    SCOPE_IDENTITY() as operation_order_id;
";
            var parameters = new List<SqlParameter>();
            var operationOrderId = 0;
            parameters.Clear();
            parameters.Add(new SqlParameter("@contract", operationOrder.Contract.Value));
            parameters.Add(new SqlParameter("@work_order", operationOrder.WorkOrder.Value));
            parameters.Add(new SqlParameter("@worker_name", operationOrder.WorkerName.Value));
            parameters.Add(new SqlParameter("@affiliation_code", operationOrder.AffiliationCode.Value));
            parameters.Add(new SqlParameter("@is_done", operationOrder.IsDone));

            SQLServerHelper.Query(sql, parameters.ToArray(), reader => { operationOrderId = Convert.ToInt32(reader["operation_order_id"]); });
            SaveOperationDetelis(operationOrder.OperationOrderDetails, operationOrderId);
        }

        private void SaveOperationDetelis(List<IOperationOrderDetail> operationOrderDetails, int operationOrderId)
        {
            var sql = @"
insert into
    [WorkTime].[dbo].[OPERATION_ORDER_DETAILS] (
        operation_order_id,
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
        @operation_order_id,
        @work_content_id,
        @work_content,

        @seg,
        @stage,
        @sfx,
        @section,

        @standard_worktime_seconds,
        @target_worktime_seconds,
        @is_done
    );
";
            var parameters = new List<SqlParameter>();
            foreach (var detail in operationOrderDetails)
            {
                parameters.Clear();
                parameters.Add(new SqlParameter("@operation_order_id", operationOrderId));
                parameters.Add(new SqlParameter("@work_content_id", detail.WorkContentId.Value));
                parameters.Add(new SqlParameter("@work_content", detail.WorkContent.Value));

                parameters.Add(new SqlParameter("@seg", detail.SEG.Value));
                parameters.Add(new SqlParameter("@stage", detail.Stage.Value));
                parameters.Add(new SqlParameter("@sfx", detail.SFX.Value));
                parameters.Add(new SqlParameter("@section", detail.Section.Value));

                parameters.Add(new SqlParameter("@standard_worktime_seconds", detail.StandardWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SqlParameter("@target_worktime_seconds", detail.TargetWorkTimeSeconds.TotalSeconds));
                parameters.Add(new SqlParameter("@is_done", detail.IsDone));

                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
        }
    }
}