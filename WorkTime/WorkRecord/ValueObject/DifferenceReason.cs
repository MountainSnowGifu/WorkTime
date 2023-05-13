using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class DifferenceReason : ValueObject<DifferenceReason>
    {
        public DifferenceReason(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "DifferenceReason" + Value;

        protected override bool EqualsCore(DifferenceReason other)
        {
            return this.Value == other.Value;
        }
    }
}
