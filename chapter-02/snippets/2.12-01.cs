// Modern .NET, Chapter 2, Section 2.12
// dotnet new console -n BaselineProbe
using System.Diagnostics;

var url = args.Length > 0 ? args[0] : "https://example.com/";
var total = args.Length > 1 ? int.Parse(args[1]) : 200;
var concurrency = args.Length > 2 ? int.Parse(args[2]) : 10;

using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var latencies = new List<double>(total);
var gate = new object();
var errors = 0;

// Warm-up: don't measure cold starts.
for (var i = 0; i < 5; i++) await http.GetAsync(url);

using var throttle = new SemaphoreSlim(concurrency);
var overall = Stopwatch.StartNew();

var tasks = Enumerable.Range(0, total).Select(async _ =>
{
    await throttle.WaitAsync();
    try
    {
        var sw = Stopwatch.StartNew();
        using var response = await http.GetAsync(url);
        sw.Stop();
        lock (gate)
        {
            if (response.IsSuccessStatusCode) latencies.Add(sw.Elapsed.TotalMilliseconds);
            else errors++;
        }
    }
    catch
    {
        lock (gate) errors++;
    }
    finally
    {
        throttle.Release();
    }
});

await Task.WhenAll(tasks);
overall.Stop();

latencies.Sort();
double P(double pct) => latencies.Count == 0 ? 0 : latencies[(int)Math.Min(latencies.Count - 1, pct / 100 * latencies.Count)];

Console.WriteLine($"URL           : {url}");
Console.WriteLine($"Requests      : {total} (concurrency {concurrency})");
Console.WriteLine($"Succeeded     : {latencies.Count}   Errors: {errors}");
Console.WriteLine($"Throughput    : {latencies.Count / overall.Elapsed.TotalSeconds:F1} req/s");
Console.WriteLine($"p50 latency   : {P(50):F1} ms");
Console.WriteLine($"p95 latency   : {P(95):F1} ms");
Console.WriteLine($"p99 latency   : {P(99):F1} ms");
