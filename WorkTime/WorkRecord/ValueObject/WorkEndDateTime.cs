using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkEndDateTime : ValueObject<WorkEndDateTime>
    {
        public WorkEndDateTime(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
        public string DisplayValue => "WorkEndDateTime" + Value.ToShortDateString();

        protected override bool EqualsCore(WorkEndDateTime other)
        {
            return this.Value == other.Value;
        }
    }
}
