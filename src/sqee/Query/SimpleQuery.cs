using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public class SimpleQuery<T> :
        IQuery<T>
        where T : class, IDocument
    {
        private SimpleQueryCriteria<T> _criteria;
        public IQueryCriteria<T> Criteria => _criteria;

        public IConnection Connection { get; }

        public IQueryResults<T> Execute()
        {
            var c = Connection.Client;

            var results = c.Search<T>(Criteria.GetSearchDescriptor());

            return new SimpleQueryResults<T>(results);
        }

        public async Task<IQueryResults<T>> ExecuteAsync()
        {
            var c = Connection.Client;

            var results = await c.SearchAsync<T>(Criteria.GetSearchDescriptor());

            return new SimpleQueryResults<T>(results);
        }

        public SimpleQuery(
            SimpleQueryCriteria<T> criteria,
            IConnection connection)
        {
            _criteria = criteria;
            Connection = connection;
        }
    }
}
