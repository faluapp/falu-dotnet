using System.Linq;
using System.Runtime.Serialization;

namespace System
{
    internal static class EnumExtensions
    {
        public static string GetEnumMemberAttrValueOrDefault<T>(this T enumVal) where T : Enum
        {
            var memInfo = typeof(T).GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false)
                              .OfType<EnumMemberAttribute>()
                              .FirstOrDefault();

            return attr?.Value ?? enumVal.ToString().ToLowerInvariant();
        }
    }
}
