// Modern .NET, Chapter 10, Section 10.14
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

const string cs = "Data Source=dataswitch.db";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShopDb>(o => o.UseSqlite(cs));
builder.Services.AddScoped<AdoCustomerRepository>(_ => new AdoCustomerRepository(cs));
builder.Services.AddScoped<EfCustomerRepository>();
builder.Services.AddScoped<ICustomerRepository>(sp =>
    builder.Configuration.GetValue<bool>("UseEfCore")
        ? sp.GetRequiredService<EfCustomerRepository>()
        : sp.GetRequiredService<AdoCustomerRepository>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDb>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
    db.Customers.AddRange(new Customer { Name = "Ada", Active = true }, new Customer { Name = "Grace", Active = true }, new Customer { Name = "Linus", Active = false });
    await db.SaveChangesAsync();
}

app.MapGet("/customers", async (ICustomerRepository repo, CancellationToken ct) => await repo.ListActiveAsync(ct));

// Shadow comparison: run BOTH implementations, return the legacy result, log any difference.
app.MapGet("/customers/compare", async (AdoCustomerRepository ado, EfCustomerRepository ef, ILogger<Program> log, CancellationToken ct) =>
{
    var legacy = await ado.ListActiveAsync(ct);
    var modern = await ef.ListActiveAsync(ct);
    var same = legacy.SequenceEqual(modern);
    if (!same) log.LogWarning("Data layer mismatch: legacy={Legacy} modern={Modern}", legacy.Count, modern.Count);
    return new { same, legacy, modern };
});

app.Run();

public sealed record CustomerDto(int Id, string Name);

public interface ICustomerRepository { Task<IReadOnlyList<CustomerDto>> ListActiveAsync(CancellationToken ct); }

public sealed class AdoCustomerRepository(string cs) : ICustomerRepository
{
    public async Task<IReadOnlyList<CustomerDto>> ListActiveAsync(CancellationToken ct)
    {
        var list = new List<CustomerDto>();
        await using var conn = new SqliteConnection(cs);
        await conn.OpenAsync(ct);
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name FROM Customers WHERE Active = 1 ORDER BY Name";
        await using var r = await cmd.ExecuteReaderAsync(ct);
        while (await r.ReadAsync(ct)) list.Add(new CustomerDto(r.GetInt32(0), r.GetString(1)));
        return list;
    }
}

public sealed class EfCustomerRepository(ShopDb db) : ICustomerRepository
{
    public async Task<IReadOnlyList<CustomerDto>> ListActiveAsync(CancellationToken ct) =>
        await db.Customers.AsNoTracking().Where(c => c.Active).OrderBy(c => c.Name)
            .Select(c => new CustomerDto(c.Id, c.Name)).ToListAsync(ct);
}

public class Customer { public int Id { get; set; } public required string Name { get; set; } public bool Active { get; set; } }
public class ShopDb(DbContextOptions<ShopDb> options) : DbContext(options) { public DbSet<Customer> Customers => Set<Customer>(); }
