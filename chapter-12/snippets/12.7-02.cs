// Modern .NET, Chapter 12, Section 12.7
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<WordCountBenchmarks>();

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class WordCountBenchmarks
{
    private string _text = "";

    [Params(100, 10_000)]
    public int Words { get; set; }

    [GlobalSetup]
    public void Setup() =>
        _text = string.Join(' ', Enumerable.Range(0, Words).Select(i => i % 7 == 0 ? "needle" : $"hay{i % 13}"));

    [Benchmark(Baseline = true)]
    public int SplitAndLinq() =>
        _text.Split(' ').Count(w => w == "needle");

    [Benchmark]
    public int SplitAndLoop()
    {
        var count = 0;
        foreach (var w in _text.Split(' ')) if (w == "needle") count++;
        return count;
    }

    [Benchmark]
    public int SpanEnumeration()
    {
        var count = 0;
        var span = _text.AsSpan();
        foreach (var range in span.Split(' '))
            if (span[range].SequenceEqual("needle")) count++;
        return count;
    }
}
