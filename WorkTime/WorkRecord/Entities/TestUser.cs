using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestUser : IOperatingUser
    {
        public TestUser(string resultWorkerName, string resultUserName, string resultNachineName)
        {
            ResultWorkerName = new ResultWorkerName(resultWorkerName);
            ResultUserName = new ResultUserName(resultUserName);
            ResultMachineName = new ResultMachineName(resultNachineName);
        }

        public ResultWorkerName ResultWorkerName { get; }
        public ResultUserName ResultUserName { get; }
        public ResultMachineName ResultMachineName { get; }
    }
}
