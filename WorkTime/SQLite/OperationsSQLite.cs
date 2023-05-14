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
    LOCAL_OPERATION_ORDERS.[remote_operation_order_id],
    LOCAL_OPERATION_ORDERS.[contract],
    LOCAL_OPERATION_ORDERS.[work_order],
    LOCAL_OPERATION_ORDERS.[worker_name],
    LOCAL_OPERATION_ORDERS.[affiliation_code],
    LOCAL_OPERATION_ORDERS.[scheduled_workdate],
    LOCAL_OPERATION_ORDERS.[is_responsible_calling],
    LOCAL_OPERATION_ORDERS.[responsible_call_reason],
    LOCAL_OPERATION_ORDERS.[is_waiting],
    LOCAL_OPERATION_ORDERS.[waitingtime_reason],
    LOCAL_OPERATION_ORDERS.[is_group_working],
    LOCAL_OPERATION_ORDERS.[group_working_order],
    LOCAL_OPERATION_ORDERS.[Is_working],
    LOCAL_OPERATION_ORDERS.[is_done]

from
    LOCAL_OPERATION_ORDERS
;
";
            var parameters = new List<SQLiteParameter>();

            var list = new List<IOperationOrder>();
            SQLiteHelper.Query(
                                            sql,
                                            parameters.ToArray(),
                                            reader =>
                                            {
                                                list.Add(new TestOperationOrder(Convert.ToInt32(reader["remote_operation_order_id"]),
                                                                                                Convert.ToString(reader["contract"]),
                                                                                                Convert.ToString(reader["work_order"]),
                                                                                                Convert.ToString(reader["worker_name"]),
                                                                                                null,
                                                                                                Convert.ToInt32(reader["affiliation_code"]),
                                                                                                Convert.ToDateTime(reader["scheduled_workdate"]),
                                                                                                Convert.ToBoolean(reader["is_responsible_calling"]),
                                                                                                Convert.ToString(reader["responsible_call_reason"]),
                                                                                                Convert.ToBoolean(reader["is_waiting"]),
                                                                                                Convert.ToString(reader["waitingtime_reason"]),
                                                                                                Convert.ToBoolean(reader["is_group_working"]),
                                                                                                Convert.ToString(reader["group_working_order"]),
                                                                                                Convert.ToBoolean(reader["Is_working"]),
                                                                                                Convert.ToBoolean(reader["is_done"])));
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

        scheduled_workdate,
        is_responsible_calling,
        responsible_call_reason,
        is_waiting,
        waitingtime_reason,
        is_group_working,
        group_working_order,
        Is_working,

        is_done
    )
values
    (
        @remote_operation_order_id,
        @contract,
        @work_order,
        @worker_name,
        @affiliation_code,

        @scheduled_workdate,
        @is_responsible_calling,
        @responsible_call_reason,
        @is_waiting,
        @waitingtime_reason,
        @is_group_working,
        @group_working_order,
        @Is_working,

        @is_done
    ) on conflict(remote_operation_order_id) do
update
set
    remote_operation_order_id = @remote_operation_order_id,
    contract = @contract,
    work_order = @work_order,
    worker_name = @worker_name,
    affiliation_code = @affiliation_code,

    scheduled_workdate = @scheduled_workdate,
    is_responsible_calling = @is_responsible_calling,
    responsible_call_reason = @responsible_call_reason,
    is_waiting = @is_waiting,
    waitingtime_reason = @waitingtime_reason,
    is_group_working = @is_group_working,
    group_working_order = @group_working_order,
    Is_working = @Is_working,

    is_done = @is_done
;";

            var parameters = new List<SQLiteParameter>();
            parameters.Clear();
            parameters.Add(new SQLiteParameter("@remote_operation_order_id", operationOrder.RemoteOperationOrderId.Value));
            parameters.Add(new SQLiteParameter("@contract", operationOrder.Contract.Value));
            parameters.Add(new SQLiteParameter("@work_order", operationOrder.WorkOrder.Value));
            parameters.Add(new SQLiteParameter("@worker_name", operationOrder.WorkerName.Value));
            parameters.Add(new SQLiteParameter("@affiliation_code", operationOrder.AffiliationCode.Value));

            parameters.Add(new SQLiteParameter("@scheduled_workdate", operationOrder.ScheduledWorkDate.Value));
            parameters.Add(new SQLiteParameter("@is_responsible_calling", operationOrder.IsResponsibleCalling));
            parameters.Add(new SQLiteParameter("@responsible_call_reason", operationOrder.ResponsibleCallReason.Value));
            parameters.Add(new SQLiteParameter("@is_waiting", operationOrder.IsWaiting));
            parameters.Add(new SQLiteParameter("@waitingtime_reason", operationOrder.WaitingTimeReason.Value));
            parameters.Add(new SQLiteParameter("@is_group_working", operationOrder.IsGroupWorking));
            parameters.Add(new SQLiteParameter("@group_working_order", operationOrder.GroupWorkingOrder.Value));
            parameters.Add(new SQLiteParameter("@Is_working", operationOrder.IsWorking));

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
        is_done,
        can_skip,
        is_skip,

        important_points,
        important_points_image,
        sqk_index,
        gen_unit
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
        @is_done,
        @can_skip,
        @is_skip,

        @important_points,
        @important_points_image,
        @sqk_index,
        @gen_unit

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
    is_done = @is_done,
    can_skip = @can_skip,
    is_skip = @is_skip,

    important_points = @important_points,
    important_points_image = @important_points_image,
    sqk_index = @sqk_index,
    gen_unit = @gen_unit
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
                parameters.Add(new SQLiteParameter("@can_skip", val.CanSkip));
                parameters.Add(new SQLiteParameter("@is_skip", val.IsSkip));

                parameters.Add(new SQLiteParameter("@important_points", val.ImportantPoints.Value));
                parameters.Add(new SQLiteParameter("@important_points_image", val.ImportantPointsImage.Value));
                parameters.Add(new SQLiteParameter("@sqk_index", val.SqkIndex.Value));
                parameters.Add(new SQLiteParameter("@gen_unit", val.GenUnit.Value));

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
