// Modern .NET, Chapter 6, Section 6.4
using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(350));

try
{
    await ProcessBatchAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Batch cancelled.");
}

static async Task ProcessBatchAsync(CancellationToken ct)
{
    for (var i = 1; i <= 10; i++)
    {
        ct.ThrowIfCancellationRequested();       // check between units of work
        Console.WriteLine($"Processing item {i}");
        await Task.Delay(100, ct);               // and pass it to async APIs
    }
}
