using Falu.Core;
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
            Sorting = "descending",
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
        Assert.Equal(new[] { "descending", "12", "123", }, dictionary.Values.SelectMany(v => v));
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
                GreaterThan = new DateTimeOffset(2021, 3, 10, 16, 41, 25, TimeSpan.Zero),
                GreaterThanOrEqualTo = new DateTimeOffset(2021, 3, 10, 19, 41, 25, TimeSpan.FromHours(3)),
                LessThan = new DateTimeOffset(2021, 3, 11, 16, 41, 25, TimeSpan.Zero),
                LessThanOrEqualTo = new DateTimeOffset(2021, 3, 11, 19, 41, 25, TimeSpan.FromHours(3)),
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
        }, dictionary.Values.SelectMany(v => v));
    }

    [Fact]
    public void Works_For_PaymentAuthorizationsListOptions()
    {
        // Prepare
        var options = new PaymentAuthorizationsListOptions
        {
            Sorting = "descending",
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
            StatusReason = new List<string> { "invalid", },
            DeclineReason = new List<string> { "insufficient_amount", },
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
            "status_reason",
            "decline_reason"
        }, dictionary.Keys);
        Assert.Equal(new[] {
            "descending",
            "12",
            "123",
            "pending",
            "closed",
            "false",
            "invalid",
            "insufficient_amount",
        }, dictionary.Values.SelectMany(v => v));
    }
}
