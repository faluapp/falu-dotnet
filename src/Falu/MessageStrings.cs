namespace Falu;

internal class MessageStrings
{
    public const string IdentitySearchDeprecated = "Identity Search has been deprecated and scheduled to be removed in a future API update. Migrate to using Identity Verifications.";

    public const string SerializationUnreferencedCodeMessage = "JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.";
    public const string SerializationRequiresDynamicCodeMessage = "JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.";
}
