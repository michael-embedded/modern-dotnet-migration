// Modern .NET, Chapter 4, Section 4.11
using Discounts;

namespace Discounts.Tests;

public class DiscountTests
{
    public static IEnumerable<object?[]> Cases() =>
    [
        [null, "gold", 0m],
        [new List<LineItem>(), "gold", 0m],
        [new List<LineItem> { new() { Sku = "A", Quantity = 2, UnitPrice = 100m } }, "gold", 30.00m],
        [new List<LineItem> { new() { Sku = "A", Quantity = 2, UnitPrice = 100m } }, "SILVER", 20.00m],
        [new List<LineItem> { new() { Sku = "A", Quantity = 1, UnitPrice = 600m } }, "bronze", 42.00m],
        [new List<LineItem> { new() { Sku = "A", Quantity = 1, UnitPrice = 100m } }, null, 0m],
        [new List<LineItem> { new() { Sku = "A", Quantity = 1, UnitPrice = 100m } }, "platinum", 0m],
    ];

    [Theory]
    [MemberData(nameof(Cases))]
    public void Legacy_ReturnsRecordedValue(List<LineItem>? items, string? tier, decimal expected)
    {
        var service = new LegacyDiscountService();
        Assert.Equal(expected, service.Calculate(items!, tier!));
    }
}
