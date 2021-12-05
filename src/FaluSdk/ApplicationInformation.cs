namespace Falu
{
    /// <summary>
    /// Information about the application ("app") which owns this integration.
    /// </summary>
    public class ApplicationInformation
    {
        ///
        public ApplicationInformation(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Name of your application.
        /// </summary>
        /// <example>SampleApp</example>
        public string Name { get; }

        /// <summary>
        /// Version of your application.
        /// </summary>
        /// <example>1.0.10</example>
        public string? Version { get; set; }

        /// <summary>
        /// Website/Url for your application.
        /// </summary>
        /// <example>https://example.com</example>
        public string? Url { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var result = Name;
            if (!string.IsNullOrWhiteSpace(Version)) result += $"/{Version}";
            if (!string.IsNullOrWhiteSpace(Url)) result += $" ({Url})";
            return result;
        }
    }
}
