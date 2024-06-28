using Falu.Serialization;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Serialization;

public class MessageTemplateModelJsonConverterTests
{
    [Theory]
    [InlineData("{\"body\":\"your car {{reg_no}} is ready for collection\",\"translations\":{},\"model\":{\"date\":\"2022-09-21T14:51:00\",\"ip\":\"127.0.0.1\",\"user_agent\":\"your-computer\"}}")]
    [InlineData("{\"body\":\"your car {{reg_no}} is ready for collection\",\"translations\":{},\"model\":{\"inner1\":{\"date\":\"2022-09-21T14:51:00\"},\"inner2\":{\"ip\":\"127.0.0.1\",\"user_agent\":\"your-computer\"}}}")]
    public void Roundtrip_Works(string expected)
    {
        var request = JsonSerializer.Deserialize(expected, FaluSerializerContext.Default.MessageTemplateValidationOptions)!;
        var actual = JsonSerializer.Serialize(request, FaluSerializerContext.Default.MessageTemplateValidationOptions);
        Assert.Equal(expected, actual);
    }
}
