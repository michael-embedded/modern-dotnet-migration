// Modern .NET, Chapter 10, Section 10.6
using Microsoft.EntityFrameworkCore;

await using (var setup = new ConcurrencyContext())
{
    await setup.Database.EnsureDeletedAsync();
    await setup.Database.EnsureCreatedAsync();
    setup.Products.Add(new Product { Name = "Keyboard", Stock = 10 });
    await setup.SaveChangesAsync();
}

// Two "users" load the same row
await using var userA = new ConcurrencyContext();
await using var userB = new ConcurrencyContext();
var a = await userA.Products.FirstAsync();
var b = await userB.Products.FirstAsync();

a.Stock -= 3;
await userA.SaveChangesAsync();                     // succeeds, version bumps

b.Stock -= 5;
try
{
    await userB.SaveChangesAsync();                 // stale version -> exception
}
catch (DbUpdateConcurrencyException ex)
{
    var entry = ex.Entries.Single();
    var dbValues = await entry.GetDatabaseValuesAsync();
    Console.WriteLine($"Conflict! Database stock is {dbValues!.GetValue<int>(nameof(Product.Stock))}, you tried to save {b.Stock}.");
    // Strategy: reload and retry, merge, or tell the user.
}

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Stock { get; set; }
    public int Version { get; set; }                // manual token for SQLite
}

public class ConcurrencyContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder o) => o.UseSqlite("Data Source=concurrency.db");

    protected override void OnModelCreating(ModelBuilder mb) =>
        mb.Entity<Product>().Property(p => p.Version).IsConcurrencyToken();

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var e in ChangeTracker.Entries<Product>().Where(e => e.State == EntityState.Modified))
            e.Entity.Version++;                      // bump the token on every update
        return base.SaveChangesAsync(ct);
    }
}
