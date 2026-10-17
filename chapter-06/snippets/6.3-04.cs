// Modern .NET, Chapter 6, Section 6.3
await foreach (var number in GenerateAsync(5))
{
    Console.WriteLine($"Received {number}");
}

static async IAsyncEnumerable<int> GenerateAsync(int count)
{
    for (var i = 1; i <= count; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}
