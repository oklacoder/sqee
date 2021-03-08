using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public class AdvancedQuery<T> :
        IQuery<T>
        where T : class, IDocument
    {
        private AdvancedQueryCriteria<T> _criteria;
        public IQueryCriteria<T> Criteria => _criteria;

        public IConnection Connection { get; }

        public AdvancedQuery(
            AdvancedQueryCriteria<T> criteria,
            IConnection connection)
        {
            _criteria = criteria;
            Connection = connection;
        }

        public IQueryResults<T> Execute()
        {
            var c = Connection.Client;

            var results = c.Search<T>(_criteria.GetSearchDescriptor());

            return new AdvancedQueryResults<T>(results);
        }

        public async Task<IQueryResults<T>> ExecuteAsync()
        {
            var c = Connection.Client;

            var results = await c.SearchAsync<T>(_criteria.GetSearchDescriptor());

            return new AdvancedQueryResults<T>(results);
        }
    }
}
