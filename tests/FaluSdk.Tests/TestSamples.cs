using Tingle.Extensions.Processing;

namespace Falu.Tests;

internal class TestSamples
{
    private const string FolderNameSamples = "Samples";

    private static Task<string> GetAsStringAsync(string fileName)
        => EmbeddedResourceHelper.GetResourceAsStringAsync<TestSamples>(FolderNameSamples, fileName)!;

    public static Task<string> GetCloudEventAsync() => GetAsStringAsync("cloud_event.json");
}
