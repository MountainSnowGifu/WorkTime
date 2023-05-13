﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.WorkRecord.ValueObject
{
    public sealed class SEG : ValueObject<SEG>
    {
        public SEG(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public string DisplayValue => "SEG" + Value;

        protected override bool EqualsCore(SEG other)
        {
            return this.Value == other.Value;
        }
    }
}
