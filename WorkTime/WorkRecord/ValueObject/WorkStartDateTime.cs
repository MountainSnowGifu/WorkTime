using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkStartDateTime : ValueObject<WorkStartDateTime>
    {
        public WorkStartDateTime(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
        public string DisplayValue => "WorkStartDateTime" + Value.ToShortDateString();

        protected override bool EqualsCore(WorkStartDateTime other)
        {
            return this.Value == other.Value;
        }
    }
}
