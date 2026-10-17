// Modern .NET, Chapter 4, Section 4.2
#nullable enable
using System.Diagnostics.CodeAnalysis;

public static class Guard
{
    public static bool TryParsePort(string? text, [NotNullWhen(true)] out string? normalized)
    {
        normalized = null;
        if (!int.TryParse(text, out var port) || port is < 1 or > 65535)
            return false;

        normalized = port.ToString();
        return true;
    }
}
