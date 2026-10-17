// Modern .NET, Chapter 12, Section 12.6
using System.Diagnostics;

ThreadPool.GetMinThreads(out var minWorker, out _);
Console.WriteLine($"Min worker threads: {minWorker} (cores: {Environment.ProcessorCount})");

var sw = Stopwatch.StartNew();
var tasks = Enumerable.Range(0, 200).Select(_ => Task.Run(() => Thread.Sleep(500))).ToArray();

while (!Task.WhenAll(tasks).IsCompleted)
{
    await Task.Delay(500);
    Console.WriteLine($"{sw.Elapsed.TotalSeconds,4:F1}s  threads={ThreadPool.ThreadCount}  queued={ThreadPool.PendingWorkItemCount}  completed={ThreadPool.CompletedWorkItemCount}");
}
Console.WriteLine($"Done in {sw.Elapsed.TotalSeconds:F1}s (ideal if unconstrained: 0.5s)");
