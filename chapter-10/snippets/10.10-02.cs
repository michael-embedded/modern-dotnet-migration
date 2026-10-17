// Modern .NET, Chapter 10, Section 10.10
using Microsoft.EntityFrameworkCore;

await using var db = new ShopDb();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();
await Seed(db);

db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

var query = db.Orders
    .Where(o => o.CustomerId == 1 && o.Status == OrderStatus.Paid)
    .Select(o => new OrderRow(o.Id, o.Customer.Name, o.Lines.Count, o.Total));

Console.WriteLine(query.ToQueryString());
var rows = await query.ToListAsync();
foreach (var r in rows.Take(3)) Console.WriteLine(r);
Console.WriteLine($"{rows.Count} rows, 1 query");

static async Task Seed(ShopDb db)
{
    var customer = new Customer { Name = "Ada" };
    for (var i = 0; i < 200; i++)
    {
        var order = new Order { Customer = customer, PlacedAtUtc = DateTime.UtcNow, Status = OrderStatus.Paid, Total = 10 + i };
        order.Lines.Add(new OrderLine { Sku = "A", Quantity = 1, UnitPrice = 10 });
        order.Lines.Add(new OrderLine { Sku = "B", Quantity = 2, UnitPrice = 5 });
        db.Orders.Add(order);
    }
    await db.SaveChangesAsync();
}

public sealed record OrderRow(int Id, string Customer, int LineCount, decimal Total);

public class Customer { public int Id { get; set; } public required string Name { get; set; } public List<Order> Orders { get; set; } = []; }
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTime PlacedAtUtc { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public List<OrderLine> Lines { get; set; } = [];
}
public class OrderLine { public int Id { get; set; } public int OrderId { get; set; } public required string Sku { get; set; } public int Quantity { get; set; } public decimal UnitPrice { get; set; } }
public enum OrderStatus { Pending, Paid, Shipped, Cancelled }

public class ShopDb : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    protected override void OnConfiguring(DbContextOptionsBuilder o) => o.UseSqlite("Data Source=after.db");
}
