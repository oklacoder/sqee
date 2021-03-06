using System;
using System.Collections.Generic;
using System.Linq;

namespace sqee
{
    public class DefaultComparator :
        IComparator
    {
        private string _Display;
        private string _Value;

        public string Value => _Value;
        public string Display => _Display;
        public DefaultComparator()
        {

        }
        public DefaultComparator(
            string display,
            string value)
        {
            _Display = display;
            _Value = value;
        }

        public static AnyWordComparator AnyWord { get; } = new AnyWordComparator();
        public static BetweenComparator Between { get; } = new BetweenComparator();
        public static EqualComparator Equal { get; } = new EqualComparator();
        public static FullPhraseComparator FullPhrase { get; } = new FullPhraseComparator();
        public static GreaterThanComparator GreaterThan { get; } = new GreaterThanComparator();
        public static GreaterThanOrEqualComparator GreaterThanOrEqual { get; } = new GreaterThanOrEqualComparator();
        public static LessThanComparator LessThan { get; } = new LessThanComparator();
        public static LessThanOrEqualComparator LessThanOrEqual { get; } = new LessThanOrEqualComparator();
        public static NotAnyWordComparator NotAnyWord { get; } = new NotAnyWordComparator();
        public static NotEqualComparator NotEqual { get; } = new NotEqualComparator();
        public static PartialPhraseComparator PartialPhrase { get; } = new PartialPhraseComparator();

        public static IEnumerable<DefaultComparator> DefaultComparators = new DefaultComparator[]
        {
            AnyWord,
            Between,
            Equal,
            FullPhrase,
            GreaterThan,
            GreaterThanOrEqual,
            LessThan,
            LessThanOrEqual,
            NotAnyWord,
            NotEqual,
            PartialPhrase
        };

        public IEnumerable<IComparator> AdditionalComparators { get; private set; } = Array.Empty<IComparator>();

        public void EnsureComparator(IComparator comparator)
        {
            var a = AdditionalComparators.ToList();
            if (!a.Any(x => comparator.Value.Equals(x.Value, StringComparison.OrdinalIgnoreCase)))
                a.Add(comparator);
            AdditionalComparators = a;
        }

        public IEnumerable<IComparator> AllComparators
        {
            get
            {
                return new IComparator[] 
                {
                    AnyWord,
                    Between,
                    Equal,
                    FullPhrase,
                    GreaterThan,
                    GreaterThanOrEqual,
                    LessThan,
                    LessThanOrEqual,
                    NotAnyWord,
                    NotEqual,
                    PartialPhrase
                }.Concat(AdditionalComparators);
            }
        }
    }

}
