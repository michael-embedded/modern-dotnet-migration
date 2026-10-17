// Modern .NET, Chapter 4, Section 4.12
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ParsingBenchmarks>();

[MemoryDiagnoser]
public class ParsingBenchmarks
{
    private const string Csv = "alpha,beta,gamma,delta,epsilon,zeta,eta,theta";

    [Benchmark(Baseline = true)]
    public int Legacy_SplitAndSubstring()
    {
        var total = 0;
        foreach (var part in Csv.Split(','))
        {
            total += part.Substring(0, 2).Length;
        }
        return total;
    }

    [Benchmark]
    public int Modern_SpanEnumeration()
    {
        var total = 0;
        foreach (var range in Csv.AsSpan().Split(','))
        {
            total += Csv.AsSpan()[range][..2].Length;
        }
        return total;
    }

    [Benchmark]
    public string[] Legacy_EmptyArray() => new string[0];

    [Benchmark]
    public string[] Modern_CollectionExpression() => [];
}
