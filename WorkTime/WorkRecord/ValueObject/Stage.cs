using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class Stage : ValueObject<Stage>
    {
        public Stage(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "Stage" + Value;

        protected override bool EqualsCore(Stage other)
        {
            return this.Value == other.Value;
        }
    }
}
