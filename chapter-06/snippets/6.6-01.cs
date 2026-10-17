// Modern .NET, Chapter 6, Section 6.6
using System.Threading.Channels;

var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(capacity: 5)
{
    FullMode = BoundedChannelFullMode.Wait      // producers wait when full: backpressure
});

var producer = Task.Run(async () =>
{
    for (var i = 1; i <= 15; i++)
    {
        await channel.Writer.WriteAsync(i);
        Console.WriteLine($"Produced {i}");
    }
    channel.Writer.Complete();                  // signal: no more items
});

var consumers = Enumerable.Range(1, 2).Select(id => Task.Run(async () =>
{
    await foreach (var item in channel.Reader.ReadAllAsync())
    {
        await Task.Delay(100);                  // simulate work
        Console.WriteLine($"  Consumer {id} handled {item}");
    }
})).ToArray();

await producer;
await Task.WhenAll(consumers);
Console.WriteLine("Done.");
