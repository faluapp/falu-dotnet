using Falu.Webhooks;
using Xunit;

namespace Falu.Tests;

public class WebhookUtilityTests
{
    private const string Secret0 = "webhook_secret";
    private const string Secret1 = "webhook_secret_expired";
    private const string Signature0 = "f675e7534ef41c1d20bc73fa2da64cde11f20cf1d3b96574d74df8a963c66f7c";
    private const string Signature1 = "16c3100ad258b8c0dc490b0e12d4347481a0b2349d172e94005e8bd161b7a89e";
    private const long KnownTimestamp = 1658299746;
    private static readonly string[] KnownSignatures = {
        $"t=1658299746,sha256={Signature0}",
        $"t=1658299746,sha256={Signature0},sha256={Signature1}",
    };

    [Fact]
    public async Task ValidateSignature_Works()
    {
        var timestamp = DateTimeOffset.FromUnixTimeSeconds(KnownTimestamp).AddSeconds(100);
        var json = await TestSamples.GetCloudEventAsync();
        WebhookUtility.ValidateSignature(json, KnownSignatures[0], Secret0, now: timestamp);
    }

    [Fact]
    public async Task ValidateSignature_Works_With_RolledSecrets()
    {
        var timestamp = DateTimeOffset.FromUnixTimeSeconds(KnownTimestamp).AddSeconds(100);
        var json = await TestSamples.GetCloudEventAsync();

        WebhookUtility.ValidateSignature(json, KnownSignatures[1], Secret0, now: timestamp);
        WebhookUtility.ValidateSignature(json, KnownSignatures[1], Secret1, now: timestamp);
    }

    [Fact]
    public async Task ValidateSignature_Rejects_OldTimestamp()
    {
        var timestamp = DateTimeOffset.FromUnixTimeSeconds(KnownTimestamp) + TimeSpan.FromSeconds(400);
        var json = await TestSamples.GetCloudEventAsync();
        var exception = Assert.Throws<FaluException>(() => WebhookUtility.ValidateSignature(json, KnownSignatures[0], Secret0, now: timestamp));
        Assert.Equal("The webhook cannot be processed because the current timestamp is outside of the allowed tolerance.", exception.Message);
    }

    [Fact]
    public void ValidateSignature_Rejects_WrongSignature()
    {
        var json = "{}"; // wrong payload
        var exception = Assert.Throws<FaluException>(() => WebhookUtility.ValidateSignature(json, KnownSignatures[0], Secret0));

        Assert.Equal("The signature for the webhook is not present in the X-Falu-Signature header.", exception.Message);
    }

    [Theory]
    [InlineData("t=,sha256")]
    [InlineData("t,sha256=")]
    [InlineData("t,sha256=,sha256=")]
    [InlineData("t,sha256=,")]
    [InlineData("t,,")]
    [InlineData(",,")]
    [InlineData("t")]
    public void ValidateSignature_Rejects_WrongValueFormats(string headerValue)
    {
        var exception = Assert.Throws<FaluException>(() => WebhookUtility.ValidateSignature("{}", headerValue, string.Empty));
        Assert.Equal("The signature header format is unexpected.", exception.Message);
    }
}
