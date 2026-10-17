// Modern .NET, Chapter 11, Section 11.2
using System.Text.Json;

public static class Snapshot
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static void Match(object value, string name, [System.Runtime.CompilerServices.CallerFilePath] string callerFile = "")
    {
        var path = Path.Combine(Path.GetDirectoryName(callerFile)!, "Snapshots", name + ".json");
        var actual = JsonSerializer.Serialize(value, Options);

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, actual);
            throw new Xunit.Sdk.XunitException($"Snapshot created at {path}. Review it, commit it, and re-run.");
        }

        Assert.Equal(File.ReadAllText(path).ReplaceLineEndings(), actual.ReplaceLineEndings());
    }
}
