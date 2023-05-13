using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class OperationId : ValueObject<OperationId>
    {
        public OperationId(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public string DisplayValue => "OperationId" + Value;

        protected override bool EqualsCore(OperationId other)
        {
            return this.Value == other.Value;
        }
    }
}
