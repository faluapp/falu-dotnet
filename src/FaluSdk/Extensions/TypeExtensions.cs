using System.Collections;

namespace System
{
    internal static class TypeExtensions
    {
        private static readonly Type[] otherPrimitives = new[] {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
        };

        public static bool IsAllowedForMessageTemplateModel(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            if (type.IsGenericType)
            {
                var gt = type.GetGenericTypeDefinition();
                if (gt == typeof(Nullable<>))
                {
                    var gta = type.GenericTypeArguments[0];
                    return IsAllowedForMessageTemplateModel(gta);
                }
            }

            if (type.IsPrimitive || otherPrimitives.Contains(type) || type.IsEnum || type.IsArray) return false;
            return !typeof(IEnumerable).IsAssignableFrom(type) || typeof(IDictionary).IsAssignableFrom(type);
        }

        public static void EnsureAllowedForMessageTemplateModel(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            if (!type.IsAllowedForMessageTemplateModel())
            {
                throw new InvalidOperationException($"Type '{type.FullName}' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>");
            }
        }
    }
}
