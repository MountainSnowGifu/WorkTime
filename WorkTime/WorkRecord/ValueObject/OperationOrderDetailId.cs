using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class OperationOrderDetailId : ValueObject<OperationOrderDetailId>
    {
        public OperationOrderDetailId(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public string DisplayValue => "OperationOrderDetailId" + Value;

        protected override bool EqualsCore(OperationOrderDetailId other)
        {
            return this.Value == other.Value;
        }
    }
}
