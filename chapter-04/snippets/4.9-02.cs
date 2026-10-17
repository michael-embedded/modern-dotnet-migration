// Modern .NET, Chapter 4, Section 4.9
// MODERN (.NET 10, C# 14)
OrderSummary[] orders =
[
    new("Ada", 1250m, OrderStatus.Paid),
    new("Grace", 80m, OrderStatus.Paid),
    new("Alan", 400m, OrderStatus.Cancelled),
    new("Linus", 310m, OrderStatus.Paid),
];

var report = OrderReport.Build(orders);

foreach (var summary in report)
{
    Console.WriteLine(OrderReport.Describe(summary));
}

public enum OrderStatus { Pending, Paid, Shipped, Cancelled }

public sealed record OrderSummary(string CustomerName, decimal Total, OrderStatus Status);

public static class OrderReport
{
    public static IReadOnlyList<OrderSummary> Build(IEnumerable<OrderSummary>? orders) =>
        orders is null
            ? []
            : [.. orders
                .Where(o => o is { Status: OrderStatus.Paid, Total: > 0 })
                .OrderByDescending(o => o.Total)];

    public static string Describe(OrderSummary o)
    {
        var label = o.Total switch
        {
            >= 1000 => "large",
            >= 100  => "medium",
            _       => "small"
        };
        return $"{o.CustomerName}: {o.Total:C} ({label})";
    }
}
