using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public interface IQuery<T>
        where T : class, IDocument
    {
        public IQueryCriteria<T> Criteria { get; }
        public IConnection Connection { get; }

        public IQueryResults<T> Execute();
        public Task<IQueryResults<T>> ExecuteAsync();
    }
}
