using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Entities;

namespace WorkTime.WorkRecord.Service
{
    public static class TestOperationService
    {
        public static List<IOperationResult> GetOperationResult(List<IOperationOrder> operationOrders)
        {
            List<IOperationResult> operationResults = new List<IOperationResult>();

            foreach (var val in operationOrders)
            {
                //operationResults.Add(new TestIOperationResult(val));
            }

            return operationResults;
        }

        public static List<IOperationOrder> GetOperations()
        {
            List<IOperationOrder> operationOrder = new List<IOperationOrder>();

            var user = new TestUser("TEST", Environment.UserName, Environment.MachineName);
            var operationDetail1 = new TestOperationOrderDetail(1, 1, "1", "test1", "seg", "stahge", "sfx", "section",
                                                                                         string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                         new TimeSpan(0, 0, 2, 0), new TimeSpan(0, 0, 1, 0), false, false, false);

            var operationDetail2 = new TestOperationOrderDetail(1, 1, "1", "test1", "seg", "stahge", "sfx", "section",
                                                                                         string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                         new TimeSpan(0, 0, 3, 0), new TimeSpan(0, 0, 4, 0), false, false, false);

            var operationDetails = new List<IOperationOrderDetail>
            {
                operationDetail1,
                operationDetail2
            };

            var operation1 = new TestOperationOrder(
                                                                            0,
                                                                            "01K",
                                                                            "FMB511",
                                                                            "akira",
                                                                            operationDetails,
                                                                            1,
                                                                            DateTime.Now,
                                                                            false,
                                                                            string.Empty,
                                                                            false,
                                                                            string.Empty,
                                                                            false,
                                                                            string.Empty,
                                                                            false,
                                                                            false);
            operationOrder.Add(operation1);

            return operationOrder;
        }
    }
}
