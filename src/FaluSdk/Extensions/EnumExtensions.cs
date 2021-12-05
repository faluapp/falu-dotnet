using System.Reflection;
using System.Runtime.Serialization;

namespace System;

internal static class EnumExtensions
{
    public static string GetEnumMemberAttrValueOrDefault<T>(this T enumVal) where T : Enum
    {
        var memInfo = typeof(T).GetMember(enumVal.ToString()).FirstOrDefault();
        var attr = memInfo?.GetCustomAttribute<EnumMemberAttribute>(inherit: false);

        return attr?.Value ?? enumVal.ToString().ToLowerInvariant();
    }
}
