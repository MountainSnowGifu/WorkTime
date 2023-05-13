using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class Contract : ValueObject<Contract>
    {
        public Contract(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "Contract" + Value;

        protected override bool EqualsCore(Contract other)
        {
            return this.Value == other.Value;
        }
    }
}
