// Modern .NET, Chapter 10, Section 10.12
using Microsoft.EntityFrameworkCore;

namespace Data;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder mb) =>
        mb.Entity<Product>(e => { e.ToTable("Products"); e.Property(p => p.Name).HasMaxLength(100); });
}

public interface IProductRepository
{
    Task<IReadOnlyList<(int Id, string Name, decimal Price)>> FindByMaxPriceAsync(decimal maxPrice, CancellationToken ct = default);
}

public sealed class EfProductRepository(CatalogContext db) : IProductRepository
{
    public async Task<IReadOnlyList<(int Id, string Name, decimal Price)>> FindByMaxPriceAsync(decimal maxPrice, CancellationToken ct = default)
    {
        var rows = await db.Products.AsNoTracking()
            .Where(p => p.Price <= maxPrice)
            .OrderBy(p => p.Price).ThenBy(p => p.Id)
            .Select(p => new { p.Id, p.Name, p.Price })
            .ToListAsync(ct);
        return [.. rows.Select(r => (r.Id, r.Name, r.Price))];
    }
}
