using Falu.MessageTemplates;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Tests;

public class MessageTemplateModelTests
{
    [Fact]
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    public void RoundTrip_Works_For_JsonSerializerOptions()
    {
        var so = SC.Default.Options;
        var original = new Dictionary<string, string> { ["name"] = "cake" };
        var model = MessageTemplateModel.Create(original, so);
        var reconverted = model.ConvertTo<Dictionary<string, string>>(so);
        Assert.False(ReferenceEquals(reconverted, original)); // different instances
        Assert.Equal(original, reconverted); // same content
    }

    [Fact]
    public void RoundTrip_Works_For_JsonTypeInfo()
    {
        var original = new Dictionary<string, string> { ["name"] = "cake" };
        var model = MessageTemplateModel.Create(original, SC.Default.DictionaryStringString);
        var reconverted = model.ConvertTo(SC.Default.DictionaryStringString);
        Assert.False(ReferenceEquals(reconverted, original)); // different instances
        Assert.Equal(original, reconverted); // same content
    }

    [Fact]
    public void RoundTrip_Works_For_JsonSerializerContext()
    {
        var original = new Dictionary<string, string> { ["name"] = "cake" };
        var model = MessageTemplateModel.Create(original, typeof(Dictionary<string, string>), SC.Default);
        var reconverted = model.ConvertTo(typeof(Dictionary<string, string>), SC.Default);
        Assert.False(ReferenceEquals(reconverted, original)); // different instances
        Assert.Equal(original, reconverted); // same content
    }

    [Theory]
    [InlineData(typeof(TestEnum), false)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(char), false)]
    [InlineData(typeof(Guid), false)]

    [InlineData(typeof(bool), false)]
    [InlineData(typeof(byte), false)]
    [InlineData(typeof(short), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(long), false)]
    [InlineData(typeof(float), false)]
    [InlineData(typeof(double), false)]
    [InlineData(typeof(decimal), false)]

    [InlineData(typeof(sbyte), false)]
    [InlineData(typeof(ushort), false)]
    [InlineData(typeof(uint), false)]
    [InlineData(typeof(ulong), false)]

    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(DateTimeOffset), false)]
    [InlineData(typeof(TimeSpan), false)]

    [InlineData(typeof(TestStruct), true)]
    [InlineData(typeof(TestClass1), true)]

    [InlineData(typeof(TestEnum?), false)]
    [InlineData(typeof(char?), false)]
    [InlineData(typeof(Guid?), false)]

    [InlineData(typeof(bool?), false)]
    [InlineData(typeof(byte?), false)]
    [InlineData(typeof(short?), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(long?), false)]
    [InlineData(typeof(float?), false)]
    [InlineData(typeof(double?), false)]
    [InlineData(typeof(decimal?), false)]

    [InlineData(typeof(sbyte?), false)]
    [InlineData(typeof(ushort?), false)]
    [InlineData(typeof(uint?), false)]
    [InlineData(typeof(ulong?), false)]

    [InlineData(typeof(DateTime?), false)]
    [InlineData(typeof(DateTimeOffset?), false)]
    [InlineData(typeof(TimeSpan?), false)]

    [InlineData(typeof(TestStruct?), true)]
    public void IsAllowedModelType_Works(Type type, bool expected)
    {
        var actual = MessageTemplateModel.IsAllowedModelType(type);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnsureAllowedModelType_Throws_InvalidOperationException()
    {
        var model = DateTimeOffset.UtcNow;

        var ex = Assert.Throws<InvalidOperationException>(() => MessageTemplateModel.EnsureAllowedModelType(model));
        var expected = "Type 'System.DateTimeOffset' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>";
        Assert.Equal(expected, ex.Message);
    }

    [Fact]
    public void EnsureAllowedModelType_Throws_InvalidOperationException_ForNullable()
    {
        var model = (DateTimeOffset?)DateTimeOffset.UtcNow;

        var ex = Assert.Throws<InvalidOperationException>(() => MessageTemplateModel.EnsureAllowedModelType(model));
        var expected = "Type 'System.DateTimeOffset' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>";
        Assert.Equal(expected, ex.Message);
    }

    [Fact]
    public void EnsureAllowedModelType_DoesNotThrow()
    {
        var model = (DateTimeOffset?)null;

        MessageTemplateModel.EnsureAllowedModelType(model);
    }

    struct TestStruct
    {
        public string Prop1 { get; set; }
        public int Prop2 { get; set; }
    }

    class TestClass1
    {
        public string? Prop1 { get; set; }
        public int Prop2 { get; set; }
    }

    enum TestEnum { TheValue }
}
