using Falu.Core;
using Falu.Identity;
using System.Collections.Generic;
using Xunit;

namespace Falu.Tests
{
    public class MarketingListOptionsTests
    {
        [Fact]
        public void PopulateQueryValues_Works_1()
        {
            // Prepare
            var options = new MarketingListOptions
            {
                Sorting = SortingOrder.Descending,
                Count = 12,
                Created = null,
                ContinuationToken = "123",
                Updated = null,
                Country = "uga",
                Gender = Gender.Female,
                Age = new RangeFilteringOptions<int>
                {
                    GreaterThanOrEqualTo = 29,
                    LessThan = 40,
                }
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
                "country",
                "gender",

                "age.lt",
                "age.gte",
            }, dictionary.Keys);
            Assert.Equal(new[] {
                "descending",
                "12",
                "123",
                "uga",
                "female",

                "40",
                "29",
            }, dictionary.Values);

        }
    }
}
