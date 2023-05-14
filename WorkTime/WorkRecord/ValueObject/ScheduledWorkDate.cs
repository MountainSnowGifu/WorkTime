using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ScheduledWorkDate : ValueObject<ScheduledWorkDate>
    {
        public ScheduledWorkDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }
        public string DisplayValue => "作業予定日" + Value.ToShortDateString();

        protected override bool EqualsCore(ScheduledWorkDate other)
        {
            return this.Value == other.Value;
        }
    }
}
