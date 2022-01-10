using Falu.Core;
using Falu.Identity;
using Falu.PaymentAuthorizations;
using Xunit;

namespace Falu.Tests;

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

    [Fact]
    public void Works_For_BasicListOptions_1()
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
    public void Works_For_BasicListOptions_2()
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

    [Fact]
    public void Works_For_MarketingListOptions()
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
            Gender = "female",
            Age = new RangeFilteringOptions<int>
            {
                GreaterThanOrEqualTo = 29,
                LessThan = 40,
            }
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

    [Fact]
    public void Works_For_PaymentAuthorizationsListOptions()
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
            Status = new List<string>
            {
                "pending",
                "closed",
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
                "approved",
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
