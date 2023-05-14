using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class GroupWorkingOrder : ValueObject<GroupWorkingOrder>
    {
        public GroupWorkingOrder(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "GroupWorkingOrder" + Value;

        protected override bool EqualsCore(GroupWorkingOrder other)
        {
            return this.Value == other.Value;
        }
    }
}
