// Modern .NET, Chapter 12, Section 12.4
static string ToHex(ReadOnlySpan<byte> bytes)
{
    Span<char> buffer = bytes.Length <= 128 ? stackalloc char[bytes.Length * 2] : new char[bytes.Length * 2];

    for (var i = 0; i < bytes.Length; i++)
        bytes[i].TryFormat(buffer.Slice(i * 2, 2), out _, "x2");

    return new string(buffer);
}

Console.WriteLine(ToHex([0xDE, 0xAD, 0xBE, 0xEF]));
