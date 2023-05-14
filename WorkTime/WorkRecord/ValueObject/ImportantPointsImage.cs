using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ImportantPointsImage : ValueObject<ImportantPointsImage>
    {
        public ImportantPointsImage(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "要領書" + Value;

        protected override bool EqualsCore(ImportantPointsImage other)
        {
            return this.Value == other.Value;
        }
    }
}
