// Modern .NET, Chapter 10, Section 10.2
using Microsoft.EntityFrameworkCore;

await using var db = new ShopContext();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

db.Customers.Add(new Customer { Name = "Ada", Email = "ada@example.com" });
db.Customers.Add(new Customer { Name = "Grace", Email = null });
await db.SaveChangesAsync();

var withEmail = await db.Customers
    .Where(c => c.Email != null)
    .OrderBy(c => c.Name)
    .ToListAsync();

foreach (var c in withEmail) Console.WriteLine($"{c.Id}: {c.Name} <{c.Email}>");

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
}

public class ShopContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite("Data Source=shop.db");
}
