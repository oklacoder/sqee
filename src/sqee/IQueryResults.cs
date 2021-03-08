using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public interface IQueryResults<T>
        where T : class, IDocument
    {
        public IEnumerable<T> Documents { get; }
        long Took { get; }
        long Total { get; }
    }
}
