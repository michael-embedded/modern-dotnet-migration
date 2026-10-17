// Modern .NET, Chapter 10, Section 10.13
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

BenchmarkRunner.Run<AccessPatterns>();

[MemoryDiagnoser]
public class AccessPatterns
{
    [GlobalSetup]
    public void Setup()
    {
        using var db = new BenchDb();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        var c = new Customer { Name = "Ada" };
        for (var i = 0; i < 1000; i++)
            db.Orders.Add(new Order { Customer = c, Total = i, Notes = new string('x', 500) });
        db.SaveChanges();
    }

    [Benchmark(Baseline = true)]
    public int Tracked_FullEntities()
    {
        using var db = new BenchDb();
        return db.Orders.Include(o => o.Customer).ToList().Count;
    }

    [Benchmark]
    public int NoTracking_FullEntities()
    {
        using var db = new BenchDb();
        return db.Orders.AsNoTracking().Include(o => o.Customer).ToList().Count;
    }

    [Benchmark]
    public int NoTracking_Projection()
    {
        using var db = new BenchDb();
        return db.Orders.AsNoTracking().Select(o => new { o.Id, o.Total, Name = o.Customer.Name }).ToList().Count;
    }

    [Benchmark]
    public int NPlusOne()
    {
        using var db = new BenchDb();
        var total = 0;
        foreach (var o in db.Orders.AsNoTracking().Take(200).ToList())
            total += db.Customers.AsNoTracking().Count(c => c.Id == o.CustomerId);
        return total;
    }

    [Benchmark]
    public int BulkUpdate_ExecuteUpdate()
    {
        using var db = new BenchDb();
        return db.Orders.Where(o => o.Total < 100).ExecuteUpdate(s => s.SetProperty(o => o.Total, o => o.Total));
    }
}

public class Customer { public int Id { get; set; } public required string Name { get; set; } }
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public decimal Total { get; set; }
    public string Notes { get; set; } = "";
}

public class BenchDb : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    protected override void OnConfiguring(DbContextOptionsBuilder o) => o.UseSqlite("Data Source=bench.db");
}
