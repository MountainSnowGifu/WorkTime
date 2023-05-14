using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class OperationOrderId : ValueObject<OperationOrderId>
    {
        public OperationOrderId(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public string DisplayValue => "OperationOrderId" + Value;

        protected override bool EqualsCore(OperationOrderId other)
        {
            return this.Value == other.Value;
        }
    }
}
