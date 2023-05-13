using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkContentId : ValueObject<WorkContentId>
    {
        public WorkContentId(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WorkContentId" + Value;

        protected override bool EqualsCore(WorkContentId other)
        {
            return this.Value == other.Value;
        }
    }
}
