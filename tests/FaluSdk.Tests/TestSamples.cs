using Tingle.Extensions.Processing;

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
