// Modern .NET, Chapter 4, Section 4.11
namespace Discounts;

public enum CustomerTier { None, Bronze, Silver, Gold }

public sealed record Line(string Sku, int Quantity, decimal UnitPrice)
{
    public decimal Total => Quantity * UnitPrice;
}

public static class DiscountCalculator
{
    private const decimal BulkThreshold = 500m;
    private const decimal BulkBonus = 0.02m;

    public static decimal Calculate(IReadOnlyCollection<Line> lines, CustomerTier tier)
    {
        if (lines is { Count: 0 }) return 0m;

        var subtotal = lines.Sum(l => l.Total);

        var rate = tier switch
        {
            CustomerTier.Gold   => 0.15m,
            CustomerTier.Silver => 0.10m,
            CustomerTier.Bronze => 0.05m,
            _                   => 0m
        };

        if (subtotal > BulkThreshold) rate += BulkBonus;

        return Math.Round(subtotal * rate, 2);
    }

    public static CustomerTier ParseTier(string? text) =>
        Enum.TryParse<CustomerTier>(text, ignoreCase: true, out var tier) ? tier : CustomerTier.None;
}
