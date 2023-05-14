using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;

namespace WorkTime.Repository
{
    public interface IOperationsRepository
    {
        List<IOperationOrder> GetOperationOrders();
        void SaveOperationOrders(IOperationOrder operationOrder);
        List<IOperationResult> GetOperationResults();
        void SaveOperationResults(List<IOperationResult> operationResults);
    }
}
