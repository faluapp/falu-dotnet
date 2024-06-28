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
        Assert.NotNull(value.Default);
        Assert.True(value.Default.HasValue);
        Assert.True(value.Default.Value);
        Assert.Null(value.Status);

        // another property is set/written
        json = "{\"status\":\"another\"}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.Null(value.Default);
        Assert.NotNull(value.Status);
        Assert.True(value.Status.HasValue);
        Assert.Equal("another", value.Status.Value);

        // both properties are set/written
        json = "{\"default\":false,\"status\":\"created\"}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.NotNull(value.Default);
        Assert.True(value.Default.HasValue);
        Assert.False(value.Default.Value);
        Assert.NotNull(value.Status);
        Assert.True(value.Status.HasValue);
        Assert.Equal("created", value.Status.Value);

        // only one property is set to null
        json = "{\"default\":null}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.NotNull(value.Default);
        Assert.True(value.Default.HasValue);
        Assert.Null(value.Default.Value);
        Assert.Null(value.Status);

        // another property is set to null
        json = "{\"status\":null}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.Null(value.Default);
        Assert.NotNull(value.Status);
        Assert.True(value.Status.HasValue);
        Assert.Null(value.Status.Value);

        // both properties are set to null
        json = "{\"default\":null,\"status\":null}";
        value = JsonSerializer.Deserialize(json, SC.Default.TestingUpdateOptions)!;
        Assert.NotNull(value.Default);
        Assert.True(value.Default.HasValue);
        Assert.Null(value.Default.Value);
        Assert.NotNull(value.Status);
        Assert.True(value.Status.HasValue);
        Assert.Null(value.Status.Value);
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

        // only one property is set to null
        value = new TestingUpdateOptions { Default = null, };
        expected = "{\"default\":null}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);

        // another property is set to null
        value = new TestingUpdateOptions { Status = null, };
        expected = "{\"status\":null}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);

        // both properties are set to null
        value = new TestingUpdateOptions { Default = null, Status = null, };
        expected = "{\"default\":null,\"status\":null}";
        actual = JsonSerializer.Serialize(value, SC.Default.TestingUpdateOptions);
        Assert.Equal(expected, actual);
    }
}
