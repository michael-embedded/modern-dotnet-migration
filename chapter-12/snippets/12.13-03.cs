// Modern .NET, Chapter 12, Section 12.13
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Text.Json;
using System.Text.RegularExpressions;

BenchmarkRunner.Run<TypicalWorkload>();

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class TypicalWorkload
{
    private readonly int[] _numbers = Enumerable.Range(0, 100_000).ToArray();
    private readonly string _json = JsonSerializer.Serialize(Enumerable.Range(0, 500).Select(i => new Item(i, $"name{i}", i * 1.5m)));
    private readonly string _text = string.Join(' ', Enumerable.Range(0, 5_000).Select(i => $"word{i} user{i}@example.com"));
    private static readonly Regex EmailRegex = new(@"[\w.]+@[\w.]+\.\w+", RegexOptions.Compiled);

    [Benchmark] public long LinqSumOfSquares() => _numbers.Where(n => n % 3 == 0).Select(n => (long)n * n).Sum();
    [Benchmark] public int JsonRoundTrip() => JsonSerializer.Deserialize<List<Item>>(_json)!.Count;
    [Benchmark] public int RegexMatches() => EmailRegex.Matches(_text).Count;
    [Benchmark] public int DictionaryHeavy()
    {
        var d = new Dictionary<int, int>();
        foreach (var n in _numbers) d[n % 1000] = d.GetValueOrDefault(n % 1000) + 1;
        return d.Count;
    }
}

public sealed record Item(int Id, string Name, decimal Price);
