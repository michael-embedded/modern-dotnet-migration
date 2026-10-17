// Modern .NET, Chapter 6, Section 6.5
using System.Diagnostics;

var ids = Enumerable.Range(1, 20).ToArray();
var sw = Stopwatch.StartNew();

await Parallel.ForEachAsync(
    ids,
    new ParallelOptions { MaxDegreeOfParallelism = 5 },
    async (id, ct) =>
    {
        await Task.Delay(200, ct);             // pretend: call an API
        Console.WriteLine($"Processed {id} on thread {Environment.CurrentManagedThreadId}");
    });

Console.WriteLine($"Total: {sw.ElapsedMilliseconds} ms");   // about 4 x 200 ms
