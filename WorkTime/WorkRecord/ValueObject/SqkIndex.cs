using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class SqkIndex : ValueObject<SqkIndex>
    {
        public SqkIndex(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "SqkIndex" + Value;

        protected override bool EqualsCore(SqkIndex other)
        {
            return this.Value == other.Value;
        }
    }
}
