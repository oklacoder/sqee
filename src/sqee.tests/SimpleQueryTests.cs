using Newtonsoft.Json;
using sqee;
using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace sqee.tests
{


    public class SimpleQueryTests
    {
        const string SampleIndex = "kibana_sample_data_ecommerce";
        [Fact]
        public void CanExecuteSimpleQuery()
        {
            var c = new Nest.ElasticClient(new Uri("http://localhost:9200"));

            var conn = new DefaultConnection(c);
            var criteria = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex });
            var query = new SimpleQuery<SampleResult>(criteria, conn);

            var results = query.Execute();
            Assert.True(results != null);
        }

        [Fact]
        public void CanExecuteSimpleQueryAsync()
        {
            var c = new Nest.ElasticClient(new Uri("http://localhost:9200"));

            var conn = new DefaultConnection(c);
            var criteria = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex });
            var query = new SimpleQuery<SampleResult>(criteria, conn);

            var results = query.ExecuteAsync().Result;
            Assert.True(results != null);
        }

        [Fact]
        public void SimpleQueryCriteriaWorksCorrectly_Take()
        {
            var c = new Nest.ElasticClient(new Uri("http://localhost:9200"));

            const int _take = 25;

            var conn = new DefaultConnection(c);
            var criteria = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex }, 0, _take);
            var query = new SimpleQuery<SampleResult>(criteria, conn);

            var results = query.Execute();
            Assert.Equal(25, results.Documents.Count());
        }
        [Fact]
        public void SimpleQueryCriteriaWorksCorrectly_Skip()
        {
            var c = new Nest.ElasticClient(new Uri("http://localhost:9200"));

            const int _take = 25;

            var conn = new DefaultConnection(c);
            var criteria = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex }, 0, _take);
            var criteria2 = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex }, 1, _take);
            var query = new SimpleQuery<SampleResult>(criteria, conn);
            var query2 = new SimpleQuery<SampleResult>(criteria2, conn);


            var results = query.Execute();
            var results2 = query2.Execute();

            var r = results.Documents.ElementAt(1) as SampleResult;
            var r2 = results2.Documents.ElementAt(0) as SampleResult;

            Assert.Equal(r.OrderId, r2.OrderId);

        }

        [Fact]
        public void SimpleQueryCriteriaWorksCorrectly_Sort()
        {
            var c = new Nest.ElasticClient(new Uri("http://localhost:9200"));

            const int _take = 25;

            var sort = new[] { new DefaultSortField("order_id", true, 0) };

            var conn = new DefaultConnection(c);
            var criteria = new SimpleQueryCriteria<SampleResult>("", new[] { SampleIndex }, 0, _take, sort);
            var query = new SimpleQuery<SampleResult>(criteria, conn);

            var results = query.Execute();

            var actualFirst = results.Documents.FirstOrDefault() as SampleResult;
            var intendedFirst = results.Documents.OrderBy(x => (x as SampleResult)?.OrderId).FirstOrDefault() as SampleResult;

            Assert.Equal(actualFirst.OrderId, intendedFirst.OrderId);
        }
    }
}
