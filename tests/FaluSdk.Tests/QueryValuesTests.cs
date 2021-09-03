using Falu.Infrastructure;
using Xunit;

namespace Falu.Tests
{
    public class QueryValuesTests
    {
        [Fact]
        public void QueryIsGenerated()
        {
            var values = new QueryValues
            {
                { "sort", "descending" },
                { "count", "100" },
                { "ct", "123" },
                { "age.lt", "40" },
                { "created.gte", "2021-03-10T19:41:25.0000000+03:00" }
            };

            var query = values.ToString();
            Assert.Equal("?sort=descending&count=100&ct=123&age.lt=40&created.gte=2021-03-10T19%3A41%3A25.0000000%2B03%3A00", query);
        }
    }
}
