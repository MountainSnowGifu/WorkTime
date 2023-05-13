using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class WaitingTimeReason : ValueObject<WaitingTimeReason>
    {
        public WaitingTimeReason(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "WaitingTimeReason" + Value;

        protected override bool EqualsCore(WaitingTimeReason other)
        {
            return this.Value == other.Value;
        }
    }
}
