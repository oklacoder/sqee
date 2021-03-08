﻿namespace sqee
{
    public class LessThanComparator :
        DefaultComparator
    {
        const string _display = "Less Than";
        const string _value = "lt";

        public LessThanComparator()
            : base(_display, _value)
        {

        }
    }

}
