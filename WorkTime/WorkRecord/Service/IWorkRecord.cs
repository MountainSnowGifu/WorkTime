using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.Service
{
    public interface IWorkRecord
    {
        DateTime WorkStartDateTime { get; set; }
        DateTime WorkEndDateTime { get; set; }
        TimeSpan WorkTimeSeconds { get; }
    }
}
