using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Entities;

namespace WorkTime.WorkRecord.Service
{
    public static class TestOperationService
    {
        public static List<IOperationResult> GetOperationResult(List<IOperation> operations)
        {
            List<IOperationResult> operationResults = new List<IOperationResult>();

            foreach (var val  in operations)
            {
                //operationResults.Add(new TestIOperationResult(val));
            }

            return operationResults;
        }

        public static List<IOperation> GetOperations()
        {
            List<IOperation> operations = new List<IOperation>();

            var user = new TestUser("TEST", Environment.UserName, Environment.MachineName);
            var operation1 = new TestOperation(1,"姿見",
                                                                                "準備",
                                                                                new TimeSpan(0, 0, 1, 0, 0),
                                                                                new TimeSpan(0, 0, 1, 0, 0),
                                                                                "01K","FMB511","120","1ST","320","","akira");
            operations.Add(operation1);

            var operation2 = new TestOperation(2,"姿見",
                                                                    "清掃",
                                                                    new TimeSpan(0, 0, 2, 0, 0),
                                                                    new TimeSpan(0, 0, 2, 0, 0),
                                                                    "01K", "FMB511", "120", "1ST", "320","","akira");
            operations.Add(operation2);

            var operation3 = new TestOperation(3,"姿見",
                                                        "片付け",
                                                        new TimeSpan(0, 0, 3, 0, 0),
                                                        new TimeSpan(0, 0, 3, 0, 0),
                                                        "01K", "FMB511", "120", "1ST", "320","","akira");

            operations.Add(operation3);

            return operations;
        }
    }
}
