using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WorkerName : ValueObject<WorkerName>
    {
        public WorkerName(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WorkerName" + Value;

        protected override bool EqualsCore(WorkerName other)
        {
            return this.Value == other.Value;
        }
    }
}
