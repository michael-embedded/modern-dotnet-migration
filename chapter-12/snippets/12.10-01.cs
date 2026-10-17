// Modern .NET, Chapter 12, Section 12.10
using System.Diagnostics;

// Build a sample log in memory: "2026-03-01T10:00:00Z|ERROR|payments|message text ..."
var services = new[] { "payments", "orders", "search", "auth" };
var levels = new[] { "INFO", "INFO", "INFO", "WARN", "ERROR" };
var rng = new Random(42);
var lines = Enumerable.Range(0, 300_000)
    .Select(i => $"2026-03-01T10:00:00Z|{levels[rng.Next(levels.Length)]}|{services[rng.Next(services.Length)]}|request {i} processed with some message text")
    .ToArray();

var legacy = Time("Legacy", () => Legacy.CountErrors(lines));
var modern = Time("Modern", () => Modern.CountErrors(lines));

Console.WriteLine(legacy.Result.OrderBy(k => k.Key).SequenceEqual(modern.Result.OrderBy(k => k.Key))
    ? "Results identical." : "RESULTS DIFFER!");

static (Dictionary<string, int> Result, long Bytes) Time(string name, Func<Dictionary<string, int>> work)
{
    work();                                                    // warm-up
    GC.Collect();
    var before = GC.GetAllocatedBytesForCurrentThread();
    var sw = Stopwatch.StartNew();
    var result = work();
    sw.Stop();
    var bytes = GC.GetAllocatedBytesForCurrentThread() - before;
    Console.WriteLine($"{name,-7} {sw.ElapsedMilliseconds,5} ms  {bytes / 1_000_000.0,8:F1} MB allocated");
    return (result, bytes);
}

static class Legacy
{
    public static Dictionary<string, int> CountErrors(string[] lines)
    {
        var counts = new Dictionary<string, int>();
        foreach (var line in lines)
        {
            var parts = line.Split('|');                       // allocates an array + 4 strings per line
            if (parts[1].ToUpper() == "ERROR")                 // allocates another string
            {
                var service = parts[2];
                counts[service] = counts.ContainsKey(service) ? counts[service] + 1 : 1;
            }
        }
        return counts;
    }
}

static class Modern
{
    public static Dictionary<string, int> CountErrors(string[] lines)
    {
        var counts = new Dictionary<string, int>();
        var lookup = counts.GetAlternateLookup<ReadOnlySpan<char>>();   // .NET 9+: look up by span, no string allocation

        foreach (var line in lines)
        {
            var span = line.AsSpan();
            var first = span.IndexOf('|');
            var rest = span[(first + 1)..];
            var second = rest.IndexOf('|');

            if (!rest[..second].Equals("ERROR", StringComparison.OrdinalIgnoreCase)) continue;

            var afterLevel = rest[(second + 1)..];
            var service = afterLevel[..afterLevel.IndexOf('|')];

            lookup[service] = lookup.TryGetValue(service, out var n) ? n + 1 : 1;   // allocates a key only for new services
        }
        return counts;
    }
}
