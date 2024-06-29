using Falu.MessageTemplates;
using System.Text.Json;
using Xunit;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Tests.Serialization;

public class OptionalConverterTests
{
    [Fact]
    public void Read_Works()
    {
        // no properties are set/written
        var json = "{}";
        var value = JsonSerializer.Deserialize(json, SC.Default.MessageTemplateUpdateOptions)!;
        Assert.Null(value.Alias);
        Assert.Null(value.Description);
        Assert.Null(value.Body);
        Assert.Null(value.Translations);
        Assert.Null(value.Metadata);

        // only 1 property is set
        json = "{\"alias\":\"alias\"}";
        value = JsonSerializer.Deserialize(json, SC.Default.MessageTemplateUpdateOptions)!;
        Assert.NotNull(value.Alias);
        Assert.True(value.Alias.HasValue);
        Assert.Equal("alias", value.Alias.Value);
        Assert.Null(value.Description);
        Assert.Null(value.Body);
        Assert.Null(value.Translations);
        Assert.Null(value.Metadata);

        // another property is set/written
        json = "{\"metadata\":{\"name\":\"falu\"}}";
        value = JsonSerializer.Deserialize(json, SC.Default.MessageTemplateUpdateOptions)!;
        Assert.Null(value.Alias);
        Assert.Null(value.Description);
        Assert.Null(value.Body);
        Assert.Null(value.Translations);
        Assert.NotNull(value.Metadata);
        Assert.True(value.Metadata.HasValue);

        // set multiple properties
        json = "{\"alias\":\"alias\",\"metadata\":{\"name\":\"falu\"}}";
        value = JsonSerializer.Deserialize(json, SC.Default.MessageTemplateUpdateOptions)!;
        Assert.NotNull(value.Alias);
        Assert.True(value.Alias.HasValue);
        Assert.Equal("alias", value.Alias.Value);
        Assert.Null(value.Description);
        Assert.Null(value.Body);
        Assert.Null(value.Translations);
        Assert.NotNull(value.Metadata);
        Assert.True(value.Metadata.HasValue);

        // set a property to null and 2 as not null
        json = "{\"alias\":\"alias\",\"description\":null,\"metadata\":{\"name\":\"falu\"}}";
        value = JsonSerializer.Deserialize(json, SC.Default.MessageTemplateUpdateOptions)!;
        Assert.NotNull(value.Alias);
        Assert.True(value.Alias.HasValue);
        Assert.Equal("alias", value.Alias.Value);
        Assert.NotNull(value.Description);
        Assert.True(value.Description.HasValue);
        Assert.Null(value.Description.Value);
        Assert.Null(value.Body);
        Assert.Null(value.Translations);
        Assert.NotNull(value.Metadata);
        Assert.True(value.Metadata.HasValue);
    }

    [Fact]
    public void Write_Works()
    {
        // no properties are set/written
        var value = new MessageTemplateUpdateOptions { };
        var expected = "{}";
        var actual = JsonSerializer.Serialize(value, SC.Default.MessageTemplateUpdateOptions);
        Assert.Equal(expected, actual);

        // only one property is set
        value = new MessageTemplateUpdateOptions { Alias = "alias" };
        expected = "{\"alias\":\"alias\"}";
        actual = JsonSerializer.Serialize(value, SC.Default.MessageTemplateUpdateOptions);
        Assert.Equal(expected, actual);

        // another property is set/written
        value = new MessageTemplateUpdateOptions
        {
            Metadata = new Dictionary<string, string>
            {
                ["name"] = "falu"
            }
        };
        expected = "{\"metadata\":{\"name\":\"falu\"}}";
        actual = JsonSerializer.Serialize(value, SC.Default.MessageTemplateUpdateOptions);
        Assert.Equal(expected, actual);

        // set multiple properties
        value = new MessageTemplateUpdateOptions
        {
            Alias = "alias",
            Metadata = new Dictionary<string, string>
            {
                ["name"] = "falu"
            }
        };
        expected = "{\"alias\":\"alias\",\"metadata\":{\"name\":\"falu\"}}";
        actual = JsonSerializer.Serialize(value, SC.Default.MessageTemplateUpdateOptions);
        Assert.Equal(expected, actual);

        // set multiple properties and have one set to null
        value = new MessageTemplateUpdateOptions
        {
            Alias = "alias",
            Description = null,
            Metadata = new Dictionary<string, string>
            {
                ["name"] = "falu"
            }
        };
        expected = "{\"alias\":\"alias\",\"description\":null,\"metadata\":{\"name\":\"falu\"}}";
        actual = JsonSerializer.Serialize(value, SC.Default.MessageTemplateUpdateOptions);
        Assert.Equal(expected, actual);
    }
}
