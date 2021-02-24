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
            return dictionary.AddIfNotNull(key, converter(value.Value));
        }

        public static IDictionary<string, string> AddIfNotNull(this IDictionary<string, string> dictionary, string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}
