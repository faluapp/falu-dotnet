namespace Falu.Tests;

internal class TestSamples
{
    private const string FolderNameSamples = "Samples";

    private static Task<string> GetAsStringAsync(string fileName)
        => EmbeddedResourceHelper.GetResourceAsStringAsync<TestSamples>(FolderNameSamples, fileName)!;

    private static Stream GetAsStreamAsync(string fileName)
        => EmbeddedResourceHelper.GetResourceAsStream<TestSamples>(FolderNameSamples, fileName)!;

    public static Task<string> GetCloudEventAsStringAsync() => GetAsStringAsync("cloud_event.json");
    public static Stream GetCloudEventAsStreamAsync() => GetAsStreamAsync("cloud_event.json");
}

internal static class EmbeddedResourceHelper
{
    public static Stream? GetResourceAsStream<T>(string resourceName) => typeof(T).Assembly.GetManifestResourceStream(resourceName);

    public static async Task<string?> GetResourceAsStringAsync<T>(string resourceName)
    {
        var st = GetResourceAsStream<T>(resourceName);
        if (st is null) return null;
        using (st)
        {
            using var reader = new StreamReader(st);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }

    public static Task<string?> GetResourceAsStringAsync<T>(string folder, string fileName)
        => GetResourceAsStringAsync<T>(string.Join(".", typeof(T).Namespace, folder, fileName));

    public static Stream? GetResourceAsStream<T>(string folder, string fileName)
        => GetResourceAsStream<T>(string.Join(".", typeof(T).Namespace, folder, fileName));
}
