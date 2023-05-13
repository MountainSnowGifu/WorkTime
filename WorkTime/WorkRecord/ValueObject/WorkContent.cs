using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkContent : ValueObject<WorkContent>
    {
        public WorkContent(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WorkContent" + Value;

        protected override bool EqualsCore(WorkContent other)
        {
            return this.Value == other.Value;
        }
    }
}
