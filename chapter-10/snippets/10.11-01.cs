// Modern .NET, Chapter 10, Section 10.11
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var counter = new QueryCounter();
await using var db = new CountingDb(counter);
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();
for (var i = 0; i < 50; i++) db.Orders.Add(new Order { Customer = new Customer { Name = $"C{i}" }, PlacedAtUtc = DateTime.UtcNow, Total = i });
await db.SaveChangesAsync();
db.ChangeTracker.Clear();

counter.Reset();
foreach (var o in await db.Orders.ToListAsync())
    await db.Entry(o).Reference(x => x.Customer).LoadAsync();         // N+1 on purpose
Console.WriteLine($"N+1 style : {counter.Count} queries");

db.ChangeTracker.Clear();
counter.Reset();
var names = await db.Orders.Select(o => o.Customer.Name).ToListAsync();
Console.WriteLine($"Projection: {counter.Count} query ({names.Count} rows)");

public sealed class QueryCounter : DbCommandInterceptor
{
    public int Count { get; private set; }
    public void Reset() => Count = 0;
    public override ValueTask<InterceptionResult<System.Data.Common.DbDataReader>> ReaderExecutingAsync(
        System.Data.Common.DbCommand command, CommandEventData eventData,
        InterceptionResult<System.Data.Common.DbDataReader> result, CancellationToken ct = default)
    { Count++; return base.ReaderExecutingAsync(command, eventData, result, ct); }
}

public class Customer { public int Id { get; set; } public required string Name { get; set; } }
public class Order { public int Id { get; set; } public Customer Customer { get; set; } = null!; public DateTime PlacedAtUtc { get; set; } public decimal Total { get; set; } }

public class CountingDb(QueryCounter counter) : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    protected override void OnConfiguring(DbContextOptionsBuilder o) =>
        o.UseSqlite("Data Source=nplus1.db").AddInterceptors(counter);
}
