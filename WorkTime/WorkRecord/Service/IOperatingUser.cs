using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperatingUser
    {
        ResultWorkerName ResultWorkerName { get; }
        ResultUserName ResultUserName { get; }
        ResultMachineName ResultMachineName { get; }
    }
}
