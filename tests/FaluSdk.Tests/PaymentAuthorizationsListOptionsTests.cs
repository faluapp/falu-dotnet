using Falu.Core;
using Falu.Infrastructure;
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
                Approved = false,
                Status = new List<PaymentAuthorizationStatus>
                {
                    PaymentAuthorizationStatus.Pending,
                    PaymentAuthorizationStatus.Closed
                },
            };

            // Act
            var query = new QueryValues();
            options.Populate(query);

            // Assert
            var dictionary = query.ToDictionary();
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
