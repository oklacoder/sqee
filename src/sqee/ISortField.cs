using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public interface ISortField
    {
        string FieldName { get; }
        bool SortAscending { get; }
        int Ordinal { get; }
    }
}
