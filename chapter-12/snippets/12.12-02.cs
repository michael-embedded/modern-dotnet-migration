// Modern .NET, Chapter 12, Section 12.12
// Report.cs
using System.Globalization;

public sealed record Line(string Sku, int Quantity, decimal UnitPrice);

public static class Report
{
    public static string Format(IReadOnlyList<Line> lines)
    {
        var result = "";
        foreach (var line in lines.OrderBy(l => l.Sku))
        {
            var total = line.Quantity * line.UnitPrice;
            result += string.Format("{0,-10} x{1,3} @ {2,8} = {3,10}\n",
                line.Sku, line.Quantity, line.UnitPrice.ToString("F2", CultureInfo.InvariantCulture), total.ToString("F2", CultureInfo.InvariantCulture));
        }
        return result;
    }
}
