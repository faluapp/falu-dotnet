using Falu.IdentityVerificationReports;
using Falu.Serialization;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Serialization;

public class JsonSnakeCaseNamingPolicyTests
{
    [Fact]
    public void Serialize_Works()
    {
        var date = new DateTimeOffset(2022, 09, 21, 14, 51, 0, TimeSpan.Zero);
        var consent = new IdentityVerificationReportConsent { Date = date, IP = "127.0.0.1", UserAgent = "your-computer", };

        var expected = @"{""date"":""2022-09-21T14:51:00+00:00"",""ip"":""127.0.0.1"",""user_agent"":""your-computer""}";
        var actual = JsonSerializer.Serialize(consent, FaluSerializerContext.Default.IdentityVerificationReportConsent);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Deserialize_Works()
    {
        var json = @"{""date"":""2022-09-21T14:51:00+00:00"",""ip"":""127.0.0.1"",""user_agent"":""your-computer""}";

        var date = new DateTimeOffset(2022, 09, 21, 14, 51, 0, TimeSpan.Zero);
        var consent = JsonSerializer.Deserialize(json, FaluSerializerContext.Default.IdentityVerificationReportConsent);
        Assert.NotNull(consent);
        Assert.Equal(date, consent.Date);
        Assert.Equal("127.0.0.1", consent.IP);
        Assert.Equal("your-computer", consent.UserAgent);
    }
}
