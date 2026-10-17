// Modern .NET, Chapter 9, Section 9.8
products.MapGet("/slow", async (CancellationToken ct) =>
{
    await Task.Delay(TimeSpan.FromSeconds(10), ct);     // stops when the client disconnects
    return "done";
});
