using Posts.Option;
using System;
using Xunit;

namespace Posts.Tests
{
    public class QueryOptionsTest
    {
        [Theory]
        [InlineData("one")]
        [InlineData("one,two")]
        [InlineData("1,2,three")]
        public void GetQueryOptions_WithInvalidQuery_ThrowsArgumentException(string query)
        {
            var options = new Options { Query = query };

            Assert.Throws<ArgumentException>(() => QueryOptions.GetQueryOptions(options));
        }

        [Theory]
        [InlineData("all")]
        [InlineData("1")]
        [InlineData("1,2")]
        public void GetQueryOptions_WithValidQuery_DoesNotThrowArgumentException(string query)
        {
            var options = new Options { Query = query };

            Assert.Null(Record.Exception(() => QueryOptions.GetQueryOptions(options)));
        }

        [Theory]
        [InlineData("all", true, null)]
        [InlineData("1", false, new[] { 1 })]
        [InlineData("1,2", false, new[] { 1, 2 })]
        public void GetQueryOptions_WithValidQuery_ParsesQueryCorrectly(string query, bool queryAll, int[] ids)
        {
            var options = new Options { Query = query };
            var queryOptions = QueryOptions.GetQueryOptions(options);

            Assert.Equal(queryAll, queryOptions.QueryAll);
            Assert.Equal(ids, queryOptions.Ids);
        }
    }
}