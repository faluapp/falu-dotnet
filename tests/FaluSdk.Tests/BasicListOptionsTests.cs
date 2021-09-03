using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace Falu.Tests
{
    public class BasicListOptionsTests
    {
        [Fact]
        public void PopulateQueryValues_Works_1()
        {
            // Prepare
            var options = new BasicListOptions
            {
                Sorting = SortingOrder.Descending,
                Count = 12,
                Created = null,
                ContinuationToken = "123",
                Updated = null,
            };

            // Act
            var query = new QueryValues();
            options.Populate(query);

            // Assert
            var dictionary = query.ToDictionary();
            Assert.NotEmpty(dictionary);
            Assert.Equal(new[] { "sort", "count", "ct", }, dictionary.Keys);
            Assert.Equal(new[] { "descending", "12", "123", }, dictionary.Values);
        }

        [Fact]
        public void PopulateQueryValues_Works_2()
        {
            // Prepare
            var options = new BasicListOptions
            {
                Sorting = null,
                Count = null,
                ContinuationToken = null,
                Updated = null,
                Created = new RangeFilteringOptions<DateTimeOffset>
                {
                    GreaterThan = DateTimeOffset.Parse("3/10/2021 4:41:25 PM +00:00"),
                    GreaterThanOrEqualTo = DateTimeOffset.Parse("3/10/2021 7:41:25 PM +03:00"),
                    LessThan = DateTimeOffset.Parse("3/11/2021 4:41:25 PM +00:00"),
                    LessThanOrEqualTo = DateTimeOffset.Parse("3/11/2021 7:41:25 PM +03:00"),
                },
            };

            // Act
            var query = new QueryValues();
            options.Populate(query);

            // Assert
            var dictionary = query.ToDictionary();
            Assert.NotEmpty(dictionary);
            Assert.Equal(new[] {
                "created.lt",
                "created.lte",
                "created.gt",
                "created.gte",
            }, dictionary.Keys);
            Assert.Equal(new[] {
                "2021-03-11T16:41:25.0000000+00:00",
                "2021-03-11T19:41:25.0000000+03:00",
                "2021-03-10T16:41:25.0000000+00:00",
                "2021-03-10T19:41:25.0000000+03:00",
            }, dictionary.Values);
        }

    }
}
