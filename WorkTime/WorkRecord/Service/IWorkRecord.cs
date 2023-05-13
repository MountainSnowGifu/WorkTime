using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Service
{
    public interface IWorkRecord
    {
        WorkStartDateTime WorkStartDateTime { get; }
        WorkEndDateTime WorkEndDateTime { get; }
        TimeSpan WorkTimeSeconds { get; }
    }
}
