// Modern .NET, Chapter 10, Section 10.8
var customerId = 1;
var orders = await db.Orders
    .FromSql($"EXEC dbo.GetOpenOrders @CustomerId = {customerId}")
    .ToListAsync();

await db.Database.ExecuteSqlAsync($"EXEC dbo.ArchiveOldOrders @Before = {DateTime.UtcNow.AddYears(-2)}");
