namespace Falu;

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
    /// For example <c>SampleApp</c>
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Version of your application.
    /// For example <c>1.0.10</c>
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Website/URL for your application.
    /// For example <c>https://example.com</c>
    /// </summary>
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
