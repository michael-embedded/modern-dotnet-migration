// Modern .NET, Chapter 10, Section 10.3
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Shop")));

// For high-throughput services, pooling reuses context instances:
// builder.Services.AddDbContextPool<ShopContext>(...);
