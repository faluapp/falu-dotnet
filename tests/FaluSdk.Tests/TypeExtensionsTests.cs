using Falu.MessageTemplates;
using System;
using Xunit;

namespace Falu.Tests
{
    public class TypeExtensionsTests
    {
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
        public void Check_Works(Type type, bool expected)
        {
            var actual = type.IsAllowedForMessageTemplateModel();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Check_Throws_InvalidOperationException()
        {
            var n = new MessageTemplateValidationRequest
            {
                Body = "Cakes",
                Model = DateTimeOffset.UtcNow,
            };

            var ex = Assert.Throws<InvalidOperationException>(() => n.Model?.GetType().EnsureAllowedForMessageTemplateModel());
            var expected = "Type 'System.DateTimeOffset' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>";
            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public void Check_Throws_InvalidOperationException_ForNullable()
        {
            var n = new MessageTemplateValidationRequest
            {
                Body = "Cakes",
                Model = (DateTimeOffset?)DateTimeOffset.UtcNow,
            };

            var ex = Assert.Throws<InvalidOperationException>(() => n.Model?.GetType().EnsureAllowedForMessageTemplateModel());
            var expected = "Type 'System.DateTimeOffset' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>";
            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public void Check_DoesNotThrow()
        {
            var n = new MessageTemplateValidationRequest
            {
                Body = "Cakes",
                Model = (DateTimeOffset?)null,
            };

            n.Model?.GetType().EnsureAllowedForMessageTemplateModel();
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
}
