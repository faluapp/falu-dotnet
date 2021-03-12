namespace System.Collections.Generic
{
    internal static class CollectionExtensions
    {
        public static IDictionary<string, string> AddIfNotNull<T>(this IDictionary<string, string> dictionary,
                                                                  string property,
                                                                  string field,
                                                                  T? value,
                                                                  Func<T, string> converter)
            where T : struct
        {
            return dictionary.AddIfNotNull($"{property}.{field}", value, converter);
        }

        public static IDictionary<string, string> AddIfNotNull<T>(this IDictionary<string, string> dictionary,
                                                                  string key,
                                                                  T? value,
                                                                  Func<T, string> converter)
            where T : struct
        {
            return dictionary.AddIfNotNull(key, value is null ? null : converter(value.Value));
        }

        public static IDictionary<string, string> AddIfNotNull(this IDictionary<string, string> dictionary, string key, string value)
        {
            if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

            if (!string.IsNullOrWhiteSpace(value))
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static T AddIf<T>(this T collection, bool condition, string value) where T : ICollection<string>
        {
            return condition ? collection.AddIfNotNull(value) : collection;
        }

        public static T AddIfNotNull<T>(this T collection, string value) where T : ICollection<string>
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
