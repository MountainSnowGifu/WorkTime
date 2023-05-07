using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.Service
{
    public interface IOperatingUser
    {
        string ResultWorkerName { get; set; }
        string ResultUserName { get; set; }
        string ResultMachineName { get; set; }
    }
}
