// Modern .NET, Chapter 10, Section 10.4
// Legacy: load every row, change it, SaveChanges
// Modern: one UPDATE statement
var affected = await db.Orders
    .Where(o => o.Status == OrderStatus.Pending && o.PlacedAtUtc < DateTime.UtcNow.AddDays(-30))
    .ExecuteUpdateAsync(s => s.SetProperty(o => o.Status, OrderStatus.Cancelled));

await db.OrderLines.Where(l => l.Quantity == 0).ExecuteDeleteAsync();
