using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkOrder : ValueObject<WorkOrder>
    {
        public WorkOrder(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WorkOrder" + Value;

        protected override bool EqualsCore(WorkOrder other)
        {
            return this.Value == other.Value;
        }
    }
}
