// Modern .NET, Chapter 6, Section 6.13
using System.Diagnostics;

ThreadPool.SetMinThreads(16, 16);
ThreadPool.SetMaxThreads(16, 16);

Console.WriteLine($"{"Concurrent requests",-20} {"Blocking (ms)",-15} {"Async (ms)",-12}");

foreach (var requests in new[] { 16, 64, 256, 1024 })
{
    var blockingMs = await Measure(requests, () => Task.Run(() => Thread.Sleep(100)));
    var asyncMs    = await Measure(requests, () => Task.Run(async () => await Task.Delay(100)));
    Console.WriteLine($"{requests,-20} {blockingMs,-15} {asyncMs,-12}");
}

static async Task<long> Measure(int requests, Func<Task> work)
{
    var sw = Stopwatch.StartNew();
    await Task.WhenAll(Enumerable.Range(0, requests).Select(_ => work()));
    return sw.ElapsedMilliseconds;
}
