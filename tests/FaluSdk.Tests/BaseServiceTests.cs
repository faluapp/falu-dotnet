using Falu.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace Falu.Tests
{
    public class BaseServiceTests
    {
        [Fact]
        public void MakeQueryString_Works()
        {
            var parameters = new Dictionary<string, string>
            {
                ["sort"] = "descending",
                ["count"] = "100",
                ["ct"] = "123",
                ["age.lt"] = "40",
                ["created.gte"] = "2021-03-10T19:41:25.0000000+03:00",
            };

            var query = BaseService.MakeQueryString(parameters);
            Assert.Equal("?sort=descending&count=100&ct=123&age.lt=40&created.gte=2021-03-10T19%3A41%3A25.0000000%2B03%3A00", query);
        }
    }
}
