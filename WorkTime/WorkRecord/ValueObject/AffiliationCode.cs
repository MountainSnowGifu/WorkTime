using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class AffiliationCode : ValueObject<AffiliationCode>
    {
        public AffiliationCode(int value)
        {
            Value = value;
        }

        public int Value { get; }
        public string DisplayValue => "所属コード" + Value;

        protected override bool EqualsCore(AffiliationCode other)
        {
            return this.Value == other.Value;
        }
    }
}
