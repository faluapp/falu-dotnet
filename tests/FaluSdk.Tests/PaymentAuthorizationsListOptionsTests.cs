using Falu.Core;
using Falu.PaymentAuthorizations;
using System.Collections.Generic;
using Xunit;

namespace Falu.Tests
{
    public class PaymentAuthorizationsListOptionsTests
    {
        [Fact]
        public void PopulateQueryValues_Works_1()
        {
            // Prepare
            var options = new PaymentAuthorizationsListOptions
            {
                Sorting = SortingOrder.Descending,
                Count = 12,
                Created = null,
                ContinuationToken = "123",
                Updated = null,
                Authorized = false,
                Status = new List<PaymentAuthorizationStatus>
                {
                    PaymentAuthorizationStatus.Pending,
                    PaymentAuthorizationStatus.Closed
                },
            };

            // Act
            var dictionary = new Dictionary<string, string>();
            options.PopulateQueryValues(dictionary);

            // Assert
            Assert.NotEmpty(dictionary);
            Assert.Equal(new[] {
                "sort",
                "count",
                "ct",
                "status",
                "authorized",
            }, dictionary.Keys);
            Assert.Equal(new[] {
                "descending",
                "12",
                "123",
                "pending,closed",
                "false",
            }, dictionary.Values);

        }
    }
}
