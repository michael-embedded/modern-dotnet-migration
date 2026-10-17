// Modern .NET, Chapter 12, Section 12.5
using System.Buffers;

static int CountNewlines(Stream stream)
{
    var buffer = ArrayPool<byte>.Shared.Rent(81_920);       // may return a LARGER array than requested
    try
    {
        var count = 0;
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            count += buffer.AsSpan(0, read).Count((byte)'\n');
        return count;
    }
    finally
    {
        ArrayPool<byte>.Shared.Return(buffer);              // ALWAYS return, even on exceptions
    }
}

var data = new MemoryStream(Enumerable.Range(0, 1_000_000).Select(i => (byte)(i % 50 == 0 ? '\n' : 'x')).ToArray());
Console.WriteLine(CountNewlines(data));
