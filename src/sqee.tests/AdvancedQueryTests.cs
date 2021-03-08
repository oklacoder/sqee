using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace sqee.tests
{
    public class AdvancedQueryTests
    {

        [Fact]
        public void AdvancedQuery_CanExecute()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();
            Assert.True(results != null);
            Assert.NotEmpty(results.Documents);
        }
        [Fact]
        public async void AdvancedQuery_CanExecuteAsync()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = await query.ExecuteAsync();
            Assert.True(results != null);
        }
        [Fact]
        public void AdvancedQuery_CriteriaWorksCorrectly_Take()
        {
            var c = TestUtil.Client;

            const int _take = 25;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                take: _take);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();
            Assert.Equal(25, results.Documents.Count());
        }
        [Fact]
        public void AdvancedQuery_CriteriaWorksCorrectly_Skip()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);

            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                skip: 0);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);
            var criteria2 = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                skip: 1);
            var query2 = new AdvancedQuery<SampleResult>(
                criteria2,
                conn);

            var results = query.Execute();
            var results2 = query2.Execute();

            var r = results.Documents.ElementAt(1) as SampleResult;
            var r2 = results2.Documents.ElementAt(0) as SampleResult;

            Assert.Equal(r.OrderId, r2.OrderId);

        }
        [Fact]
        public void AdvancedQuery_CriteriaWorksCorrectly_Sort()
        {
            var c = TestUtil.Client;

            var sort = new[] { new DefaultSortField("order_id", true, 0) };

            var conn = new DefaultConnection(c);

            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                sort);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var actualFirst = results.Documents.FirstOrDefault() as SampleResult;
            var intendedFirst = results.Documents.OrderBy(x => (x as SampleResult)?.OrderId).FirstOrDefault() as SampleResult;

            Assert.Equal(actualFirst.OrderId, intendedFirst.OrderId);
        }
        [Fact]
        public void AdvancedQuery_ReturnsAllFieldsByDefault()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var hasValues = TestUtil.DoAllTheseFieldsHaveValues(
                results.Documents.FirstOrDefault(),
                TestUtil.GetObjectFields(results.Documents.FirstOrDefault()).ToArray());

            Assert.True(hasValues);
        }
        [Fact]
        public void AdvancedQuery_ReturnsOnlySpecifiedFields()
        {
            var c = TestUtil.Client;

            const string fieldToUse = "customer_full_name";//nameof(SampleResult.CustomerFullName);// 

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                returnFields: new[] { new DefaultReturnField(fieldToUse) });
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var hasValues = TestUtil.DoOnlyTheseFieldsHaveValues(
                results.Documents.FirstOrDefault(),
                nameof(SampleResult.CustomerFullName));

            Assert.True(hasValues);
        }
        [Fact]
        public void AdvancedQuery_ReturnsNoFieldsWhenEmptySpecified()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var hasValues = TestUtil.DoAllTheseFieldsHaveValues(
                results.Documents.FirstOrDefault(),
                TestUtil.GetObjectFields(results.Documents.FirstOrDefault()).ToArray());

            Assert.True(hasValues);
        }
        [Fact]
        public void AdvancedQuery_ReturnsAllRecordsWhenNoFilterSpecified()
        {
            var c = TestUtil.Client;

            const int expectedTotal = 4675;

            var conn = new DefaultConnection(c);

            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                null,
                null,
                null);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            Assert.Equal(expectedTotal, results.Total);
        }
        [Fact]
        public void AdvancedQuery_ReturnsOnlyMatchingRecordsWhenFilterSpecified_AnyWord()
        {
            var c = TestUtil.Client;

            const string valToMatch = "Basic";
            var filter = new DefaultFilterField(DefaultComparator.AnyWord, valToMatch, "products.product_name");

            var conn = new DefaultConnection(c);

            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                null,
                null,
                new[] { filter });
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var all = results.Documents.All(
                x => x.Products.Any(
                    z => z.ProductName.Contains(valToMatch, StringComparison.OrdinalIgnoreCase)));
            Assert.NotEmpty(results.Documents);
            Assert.True(all);
        }
        [Fact]
        public void AdvancedQuery_ReturnsOnlyMatchingRecordsWhenFilterSpecified_Equal()
        {
            var c = TestUtil.Client;

            const string valToMatch = "Eddie";
            var filter = new DefaultFilterField(DefaultComparator.Equal, valToMatch, "customer_first_name");

            var conn = new DefaultConnection(c);

            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                null,
                null,
                new[] { filter });
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();

            var all = results.Documents.All(x => x.CustomerFirstName == valToMatch);
            
            Assert.NotEmpty(results.Documents);
            Assert.True(all);            
        }
        [Fact]
        public void AdvancedQuery_ReturnsBucketsForSpecifiedFields()
        {
            var c = TestUtil.Client;

            var b = new[] { new DefaultBucketField("manufacturer.keyword") };

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                bucketFields: b);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();
            Assert.True(results != null);
            Assert.NotEmpty(results.Documents);
            Assert.NotEmpty((results as AdvancedQueryResults<SampleResult>)?.Buckets);
        }
        [Fact]
        public void AdvancedQuery_ReturnsBucketsForSpecifiedFieldsButNoDocumentsWhenZeroPageSize()
        {
            var c = TestUtil.Client;

            var b = new[] { new DefaultBucketField("manufacturer.keyword") };

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices,
                bucketFields: b,
                take: 0);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();
            Assert.True(results != null);
            Assert.Empty(results.Documents);
            Assert.NotEmpty((results as AdvancedQueryResults<SampleResult>)?.Buckets);
        }


        [Fact]
        public void AdvancedQuery_ReturnsNoBucketsWhenNoFieldsSpecified()
        {
            var c = TestUtil.Client;

            var conn = new DefaultConnection(c);
            var criteria = new AdvancedQueryCriteria<SampleResult>(
                TestUtil.SampleIndices);
            var query = new AdvancedQuery<SampleResult>(
                criteria,
                conn);

            var results = query.Execute();
            Assert.True(results != null);
            Assert.NotEmpty(results.Documents);
            Assert.Empty((results as AdvancedQueryResults<SampleResult>)?.Buckets);
        }
    }


    internal static class TestUtil
    {

        const string SampleIndex = "kibana_sample_data_ecommerce";
        const string ClusterUrl = "http://localhost:9200";
        internal static Nest.ElasticClient Client => new Nest.ElasticClient(new Uri(ClusterUrl));
        internal static string[] SampleIndices => new[] { SampleIndex };


        internal static IEnumerable<string> GetObjectFields(object obj)
        {
            return obj.GetType().GetProperties().Select(x => x.Name);
        }
        internal static bool DoAllTheseFieldsHaveValues(object obj, params string[] fields)
        {
            var res = true;
            var props = obj.GetType().GetProperties();

            foreach(var p in props.Where(x => 
                fields.Any(z => 
                    z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && p.IsPropertyEmpty(val);
            }

            return res;
        }
        internal static bool DoAllTheseFieldsHaveNoValues(object obj, params string[] fields)
        {
            var res = true;
            var props = obj.GetType().GetProperties();

            foreach (var p in props.Where(x =>
                 fields.Any(z =>
                     z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && p.IsPropertyEmpty(val);
            }

            return res;
        }
        internal static bool DoAllButTheseFieldsHaveValues(object obj, params string[] fields)
        {
            var res = true;
            var props = obj.GetType().GetProperties();

            foreach (var p in props.Where(x =>
                 fields.Any(z =>
                     !z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && p.IsPropertyEmpty(val);
            }
            foreach (var p in props.Where(x =>
                 fields.Any(z =>
                     z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && p.IsPropertyEmpty(val);
            }

            return res;
        }
        internal static bool DoOnlyTheseFieldsHaveValues(object obj, params string[] fields)
        {
            var res = true;
            var props = obj.GetType().GetProperties();

            foreach (var p in props.Where(x =>
                 fields.Any(z =>
                     !z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && !p.IsPropertyEmpty(val);                
            }
            foreach (var p in props.Where(x =>
                 fields.Any(z =>
                     z.Equals(x.Name, StringComparison.OrdinalIgnoreCase))))
            {
                var val = p.GetValue(obj)?.ToString();
                res = res && p.IsPropertyEmpty(val);
            }

            return res;
        }

        private static bool IsPropertyEmpty(this PropertyInfo p, string val)
        {
            var res = true;
            if (val == null)
                return false;

            var t = p.PropertyType;

            if (t == typeof(DateTime))
            {
                res = DateTime.Parse(val) != DateTime.MinValue;
            }
            else if (t == typeof(DateTime?))
            {
                res = DateTime.Parse(val) != DateTime.MinValue;
            }

            return res;
        }
    }
}
