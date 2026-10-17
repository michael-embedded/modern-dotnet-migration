// Modern .NET, Chapter 12, Section 12.2
using System.Diagnostics;

static void Report(string label, Action work)
{
    GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
    var gen0 = GC.CollectionCount(0);
    var gen1 = GC.CollectionCount(1);
    var gen2 = GC.CollectionCount(2);
    var before = GC.GetAllocatedBytesForCurrentThread();
    var sw = Stopwatch.StartNew();

    work();

    sw.Stop();
    Console.WriteLine($"{label,-22} {sw.ElapsedMilliseconds,5} ms  " +
                      $"allocated {(GC.GetAllocatedBytesForCurrentThread() - before) / 1_000_000.0,8:F1} MB  " +
                      $"GCs: gen0={GC.CollectionCount(0) - gen0} gen1={GC.CollectionCount(1) - gen1} gen2={GC.CollectionCount(2) - gen2}");
}

Report("Many small objects", () =>
{
    long sum = 0;
    for (var i = 0; i < 5_000_000; i++)
        sum += new Point(i, i).X;                       // class: a heap allocation each iteration
});

Report("Struct (no allocation)", () =>
{
    long sum = 0;
    for (var i = 0; i < 5_000_000; i++)
        sum += new PointStruct(i, i).X;                 // struct: lives on the stack
});

Report("Large arrays (LOH)", () =>
{
    for (var i = 0; i < 2_000; i++)
    {
        var buffer = new byte[100_000];                 // above the LOH threshold
        buffer[0] = 1;
    }
});

Console.WriteLine($"GC mode: {(System.Runtime.GCSettings.IsServerGC ? "Server" : "Workstation")}, " +
                  $"latency: {System.Runtime.GCSettings.LatencyMode}");

sealed class Point(int x, int y) { public int X { get; } = x; public int Y { get; } = y; }
readonly struct PointStruct(int x, int y) { public int X { get; } = x; public int Y { get; } = y; }
