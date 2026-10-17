// Modern .NET, Chapter 7, Section 7.6
var temp = Path.Combine(Path.GetTempPath(), "sample.log");
await File.WriteAllLinesAsync(temp, Enumerable.Range(1, 1000).Select(i => i % 7 == 0 ? $"ERROR line {i}" : $"INFO line {i}"));

var errors = 0;
await foreach (var line in File.ReadLinesAsync(temp))
{
    if (line.StartsWith("ERROR", StringComparison.Ordinal)) errors++;
}
Console.WriteLine($"{errors} error lines");
