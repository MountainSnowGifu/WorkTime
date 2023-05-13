using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class ResultWorkerName : ValueObject<ResultWorkerName>
    {
        public ResultWorkerName(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "氏名" + Value;

        protected override bool EqualsCore(ResultWorkerName other)
        {
            return this.Value == other.Value;
        }
    }
}
