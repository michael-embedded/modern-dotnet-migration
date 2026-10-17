// Modern .NET, Chapter 12, Section 12.12
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ReportBenchmarks>();

[MemoryDiagnoser]
public class ReportBenchmarks
{
    private readonly List<Line> _lines = Enumerable.Range(0, 50)
        .Select(i => new Line($"SKU{(i * 7) % 50:D3}", i % 5 + 1, 9.99m + i)).ToList();

    [Benchmark(Baseline = true)]
    public string Original() => Report.Format(_lines);

    [Benchmark]
    public string Improved() => ReportFast.Format(_lines);
}
