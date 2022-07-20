using Falu.Webhooks;
using Xunit;

namespace Falu.Tests;

public class WebhookUtilityTests
{
    private const string Secret0 = "webhook_secret";
    private const string Secret1 = "webhook_secret_expired";
    private const string Signature0 = "a950948ee5a9ce80d6d0d250dba2723be4a62319d936b280992f804ee9b35566";
    private const string Signature1 = "684ed18f8c9ca820c9fd079b83ed1fc5ddbf70e4e512b2ca5a2761a72c402a21";
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
        var exception = Assert.Throws<FaluException>(() => WebhookUtility.ValidateSignature("{}", KnownSignatures[0], Secret0));
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
