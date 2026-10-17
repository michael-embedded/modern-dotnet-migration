// Modern .NET, Chapter 4, Section 4.4
var order = new Order(7, 120m, OrderStatus.Paid);

Console.WriteLine(Shipping.Calculate(order, "US", isPremium: false)); // 0
Console.WriteLine(Shipping.Calculate(order, "MX", isPremium: false)); // 14.99
Console.WriteLine(Shipping.Calculate(order, "DE", isPremium: true));  // 0

public enum OrderStatus { Pending, Paid, Shipped, Cancelled }
public sealed record Order(int Id, decimal Total, OrderStatus Status);

public static class Shipping
{
    public static decimal Calculate(Order order, string country, bool isPremium) =>
        (isPremium, country, order.Total) switch
        {
            (true, _, _)                       => 0m,
            (false, "US", > 100)               => 0m,
            (false, "US", > 50)                => 4.99m,
            (false, "US", _)                   => 9.99m,
            (false, "CA" or "MX", > 150)       => 0m,
            (false, "CA" or "MX", _)           => 14.99m,
            _                                  => 29.99m
        };
}
