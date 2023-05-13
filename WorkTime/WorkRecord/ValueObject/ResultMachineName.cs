using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ResultMachineName : ValueObject<ResultMachineName>
    {
        public ResultMachineName(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "ResultMachineName" + Value;

        protected override bool EqualsCore(ResultMachineName other)
        {
            return this.Value == other.Value;
        }
    }
}
