using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTime.WorkRecord.Service;

namespace WorkTime.WorkRecord.Entities
{
    public sealed class TestWorkRecord : IWorkRecord
    {
        public TestWorkRecord(
                                         DateTime workStartDateTime,
                                         DateTime workEndDateTime)
        {
            WorkStartDateTime = workStartDateTime;
            WorkEndDateTime = workEndDateTime;
        }

        public DateTime WorkStartDateTime { get; set; }
        public DateTime WorkEndDateTime { get; set; }
        public TimeSpan WorkTimeSeconds
        {
            get
            {
                return WorkEndDateTime - WorkStartDateTime;
            }
        }
    }
}
