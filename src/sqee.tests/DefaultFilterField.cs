﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee.tests
{
    public class DefaultFilterField :
        IFilterField
    {
        public string FieldName { get; }

        public string Value { get; }

        public IComparator Comparator { get; }

        public DefaultFilterField()
        {

        }
        public DefaultFilterField(
            IComparator comparator,
            string value,
            string fieldName)
        {
            Comparator = comparator;
            Value = value;
            FieldName = fieldName;
        }
    }
}
