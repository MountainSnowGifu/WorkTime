using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class GenUnit : ValueObject<GenUnit>
    {
        public GenUnit(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "GenUnit" + Value;

        protected override bool EqualsCore(GenUnit other)
        {
            return this.Value == other.Value;
        }
    }
}
