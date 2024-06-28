using Falu.Core;
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
        var value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.Null(value.Default);
        Assert.Null(value.Status);

        // only one property is set/written
        json = "{\"default\":true}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.True(value.Default.HasValue);
        Assert.True(value.Default.Value.Value);
        Assert.Null(value.Status);

        // another property is set/written
        json = "{\"status\":\"another\"}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.Null(value.Default);
        Assert.True(value.Status.HasValue);
        Assert.Equal("another", value.Status.Value);

        // both properties are set/written
        json = "{\"default\":false,\"status\":\"created\"}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.True(value.Default.HasValue);
        Assert.False(value.Default.Value.Value);
        Assert.True(value.Status.HasValue);
        Assert.Equal("created", value.Status.Value);
    }

    [Fact]
    public void Write_Works()
    {
        // no properties are set/written
        var value = new TestingUpdateOptions { };
        var expected = "{}";
        var actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);

        // only one property is set/written
        value = new TestingUpdateOptions { Default = true, };
        expected = "{\"default\":true}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);

        // another property is set/written
        value = new TestingUpdateOptions { Status = "another", };
        expected = "{\"status\":\"another\"}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);

        // both properties are set/written
        value = new TestingUpdateOptions { Default = false, Status = "created", };
        expected = "{\"default\":false,\"status\":\"created\"}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);
    }
}
