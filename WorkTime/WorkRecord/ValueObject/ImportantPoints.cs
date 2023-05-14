using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ImportantPoints : ValueObject<ImportantPoints>
    {
        public ImportantPoints(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "注意事項" + Value;

        protected override bool EqualsCore(ImportantPoints other)
        {
            return this.Value == other.Value;
        }
    }
}
