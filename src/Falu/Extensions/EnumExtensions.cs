using System.Linq;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
    internal static class EnumExtensions
    {
        public static string? GetEnumMemberAttrValue<T>(this T enumVal) where T : Enum
        {
            var memInfo = typeof(T).GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false)
                              .OfType<EnumMemberAttribute>()
                              .FirstOrDefault();

            return attr?.Value;
        }

        public static string GetEnumMemberAttrValueOrDefault<T>(this T enumVal) where T : Enum
        {
            return enumVal.GetEnumMemberAttrValue() ?? enumVal.ToString().ToLowerInvariant();
        }
    }
}
