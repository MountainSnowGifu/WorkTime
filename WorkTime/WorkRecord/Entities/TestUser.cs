using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestUser : IOperatingUser
    {
        public TestUser(string resultWorkerName, string resultUserName, string resultNachineName)
        {
            ResultWorkerName = resultWorkerName;
            ResultUserName = resultUserName;
            ResultMachineName = resultNachineName;
        }

        public string ResultWorkerName { get; set; }
        public string ResultUserName { get; set; }
        public string ResultMachineName { get; set; }
    }
}
