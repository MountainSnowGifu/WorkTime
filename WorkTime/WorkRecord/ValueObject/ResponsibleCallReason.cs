using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ResponsibleCallReason : ValueObject<ResponsibleCallReason>
    {
        public ResponsibleCallReason(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "呼び出し理由" + Value;

        protected override bool EqualsCore(ResponsibleCallReason other)
        {
            return this.Value == other.Value;
        }
    }
}
