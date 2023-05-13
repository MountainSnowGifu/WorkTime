using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;
using WorkTime.WorkRecord.ValueObject;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestWorkRecord : IWorkRecord
    {
        public TestWorkRecord(
                                         DateTime workStartDateTime,
                                         DateTime workEndDateTime)
        {
            WorkStartDateTime = new WorkStartDateTime(workStartDateTime);
            WorkEndDateTime = new WorkEndDateTime(workEndDateTime);
        }

        public WorkStartDateTime WorkStartDateTime { get; }
        public WorkEndDateTime WorkEndDateTime { get; }
        public TimeSpan WorkTimeSeconds
        {
            get
            {
                return WorkEndDateTime.Value - WorkStartDateTime.Value;
            }
        }
    }
}
