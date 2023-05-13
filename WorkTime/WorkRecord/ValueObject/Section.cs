using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class Section : ValueObject<Section>
    {
        public Section(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "Section" + Value;

        protected override bool EqualsCore(Section other)
        {
            return this.Value == other.Value;
        }
    }
}
