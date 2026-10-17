// Modern .NET, Chapter 4, Section 4.7
var orders = new[]
{
    new Order(1, 20m, OrderStatus.Paid),
    new Order(2, 75m, OrderStatus.Paid),
    new Order(3, 40m, OrderStatus.Cancelled),
    new Order(4, 90m, OrderStatus.Pending),
};

var biggest = orders.MaxBy(o => o.Total);
var byStatus = orders.CountBy(o => o.Status);
var batches = orders.Chunk(2);

Console.WriteLine(biggest);
foreach (var (status, count) in byStatus) Console.WriteLine($"{status}: {count}");
Console.WriteLine($"Batches: {batches.Count()}");

public enum OrderStatus { Pending, Paid, Shipped, Cancelled }
public sealed record Order(int Id, decimal Total, OrderStatus Status);
