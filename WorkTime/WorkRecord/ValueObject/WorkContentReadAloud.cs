using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkContentReadAloud : ValueObject<WorkContentReadAloud>
    {
        public WorkContentReadAloud(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WorkContentReadAloud" + Value;

        protected override bool EqualsCore(WorkContentReadAloud other)
        {
            return this.Value == other.Value;
        }
    }
}
