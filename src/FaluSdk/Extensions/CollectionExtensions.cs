namespace System.Collections.Generic
{
    internal static class CollectionExtensions
    {
        public static T AddIf<T>(this T collection, bool condition, string value) where T : ICollection<string>
        {
            return condition ? collection.AddIfNotNull(value) : collection;
        }

        public static T AddIfNotNull<T>(this T collection, string? value) where T : ICollection<string>
        {
            if (collection is null) throw new ArgumentNullException(nameof(collection));

            if (!string.IsNullOrWhiteSpace(value))
            {
                collection.Add(value);
            }

            return collection;
        }
    }
}
