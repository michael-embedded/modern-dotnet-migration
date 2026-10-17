// Modern .NET, Chapter 14, Section 14.9
// dotnet new console -n BaselineCompare
using System.Diagnostics;

if (args.Length < 2) { Console.WriteLine("Usage: BaselineCompare <legacyUrl> <modernUrl> [requests=500] [concurrency=20]"); return 2; }

var requests = args.Length > 2 ? int.Parse(args[2]) : 500;
var concurrency = args.Length > 3 ? int.Parse(args[3]) : 20;

var legacy = await Run("legacy", args[0]);
var modern = await Run("modern", args[1]);

Console.WriteLine();
Console.WriteLine($"{"Metric",-14} {"Legacy",12} {"Modern",12} {"Change",10}");
Row("Throughput/s", legacy.Rps, modern.Rps, higherIsBetter: true);
Row("p50 ms", legacy.P50, modern.P50, higherIsBetter: false);
Row("p95 ms", legacy.P95, modern.P95, higherIsBetter: false);
Row("p99 ms", legacy.P99, modern.P99, higherIsBetter: false);
Console.WriteLine($"{"Errors",-14} {legacy.Errors,12} {modern.Errors,12}");
return 0;

static void Row(string name, double a, double b, bool higherIsBetter)
{
    var change = a == 0 ? 0 : (b - a) / a * 100;
    var good = higherIsBetter ? change >= 0 : change <= 0;
    Console.WriteLine($"{name,-14} {a,12:F1} {b,12:F1} {change,+9:F1}% {(good ? "ok" : "WORSE")}");
}

async Task<(double Rps, double P50, double P95, double P99, int Errors)> Run(string label, string url)
{
    using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
    for (var i = 0; i < 10; i++) { try { await http.GetAsync(url); } catch { } }      // warm-up

    var latencies = new List<double>(requests);
    var errors = 0;
    var gate = new object();
    using var throttle = new SemaphoreSlim(concurrency);
    var overall = Stopwatch.StartNew();

    await Task.WhenAll(Enumerable.Range(0, requests).Select(async _ =>
    {
        await throttle.WaitAsync();
        try
        {
            var sw = Stopwatch.StartNew();
            using var r = await http.GetAsync(url);
            sw.Stop();
            lock (gate) { if (r.IsSuccessStatusCode) latencies.Add(sw.Elapsed.TotalMilliseconds); else errors++; }
        }
        catch { lock (gate) errors++; }
        finally { throttle.Release(); }
    }));

    overall.Stop();
    latencies.Sort();
    double P(double pct) => latencies.Count == 0 ? 0 : latencies[(int)Math.Min(latencies.Count - 1, pct / 100 * latencies.Count)];
    Console.WriteLine($"{label}: {latencies.Count} ok, {errors} errors");
    return (latencies.Count / overall.Elapsed.TotalSeconds, P(50), P(95), P(99), errors);
}
