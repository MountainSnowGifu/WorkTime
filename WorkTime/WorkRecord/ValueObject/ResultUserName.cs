using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ResultUserName : ValueObject<ResultUserName>
    {
        public ResultUserName(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "USER" + Value;

        protected override bool EqualsCore(ResultUserName other)
        {
            return this.Value == other.Value;
        }
    }
}
