namespace Falu.Tests;

internal class TestSamples
{
    public static Task<string> GetCloudEventAsStringAsync() => GetAsStringAsync("cloud_event.json");
    public static Stream GetCloudEventAsStreamAsync() => GetAsStreamAsync("cloud_event.json");

    private static Task<string> GetAsStringAsync(string fileName)
        => GetResourceAsStringAsync<TestSamples>($"{typeof(TestSamples).Namespace}.Samples.{fileName}")!;

    private static Stream GetAsStreamAsync(string fileName)
        => GetResourceAsStream<TestSamples>($"{typeof(TestSamples).Namespace}.Samples.{fileName}")!;

    public static async Task<string?> GetResourceAsStringAsync<T>(string resourceName)
    {
        var st = GetResourceAsStream<T>(resourceName);
        if (st is null) return null;
        using (st)
        {
            using var reader = new StreamReader(st, detectEncodingFromByteOrderMarks: true);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }
    }

    public static Stream? GetResourceAsStream<T>(string resourceName) => typeof(T).Assembly.GetManifestResourceStream(resourceName);
}
