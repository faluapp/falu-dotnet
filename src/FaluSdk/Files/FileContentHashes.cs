namespace Falu.Files;

/// <summary>Represents various hashes of a file.</summary>
public class FileContentHashes
{
    /// <summary>
    /// Plain hash using <see href="https://en.wikipedia.org/wiki/MD5">MD5</see> in hex.
    /// </summary>
    public string? Md5 { get; set; }

    /// <summary>
    /// Plain hash using <see href="https://en.wikipedia.org/wiki/SHA-2">SHA256</see> in hex.
    /// </summary>
    public string? Sha256 { get; set; }
}
