// Modern .NET, Chapter 6, Section 6.10
using System.Diagnostics;

var sw = Stopwatch.StartNew();
var legacy = LegacyWorkflow.Run(Enumerable.Range(1, 10));
Console.WriteLine($"Legacy: total={legacy} in {sw.ElapsedMilliseconds} ms");

sw.Restart();
var modern = await ModernWorkflow.RunAsync(Enumerable.Range(1, 10));
Console.WriteLine($"Modern: total={modern} in {sw.ElapsedMilliseconds} ms");

static class FakeApi
{
    public static decimal GetBalance(int id) { Thread.Sleep(200); return id * 10m; }
    public static async Task<decimal> GetBalanceAsync(int id, CancellationToken ct)
    {
        await Task.Delay(200, ct);
        return id * 10m;
    }
}

static class LegacyWorkflow
{
    public static decimal Run(IEnumerable<int> ids)
    {
        decimal total = 0;
        foreach (var id in ids)
        {
            total += FakeApi.GetBalance(id);    // 10 x 200 ms, one after another
        }
        return total;
    }
}

static class ModernWorkflow
{
    public static async Task<decimal> RunAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        var results = new System.Collections.Concurrent.ConcurrentBag<decimal>();

        await Parallel.ForEachAsync(
            ids,
            new ParallelOptions { MaxDegreeOfParallelism = 5, CancellationToken = ct },
            async (id, token) => results.Add(await FakeApi.GetBalanceAsync(id, token)));

        return results.Sum();
    }
}
