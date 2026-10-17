// Modern .NET, Chapter 12, Section 12.12
// ReportFast.cs
using System.Globalization;
using System.Text;

public static class ReportFast
{
    public static string Format(IReadOnlyList<Line> lines)
    {
        var sorted = new Line[lines.Count];
        for (var i = 0; i < sorted.Length; i++) sorted[i] = lines[i];
        Array.Sort(sorted, static (a, b) => string.CompareOrdinal(a.Sku, b.Sku));

        var sb = new StringBuilder(lines.Count * 48);
        Span<char> number = stackalloc char[32];

        foreach (var line in sorted)
        {
            sb.Append(line.Sku.PadRight(10)).Append(" x");
            AppendPadded(sb, line.Quantity, 3, number);
            sb.Append(" @ ");
            AppendPadded(sb, line.UnitPrice, 8, number);
            sb.Append(" = ");
            AppendPadded(sb, line.Quantity * line.UnitPrice, 10, number);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    private static void AppendPadded<T>(StringBuilder sb, T value, int width, Span<char> scratch) where T : ISpanFormattable
    {
        value.TryFormat(scratch, out var written, "F2".AsSpan()[..(typeof(T) == typeof(int) ? 0 : 2)], CultureInfo.InvariantCulture);
        sb.Append(' ', Math.Max(0, width - written)).Append(scratch[..written]);
    }
}
