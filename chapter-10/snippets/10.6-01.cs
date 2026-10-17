// Modern .NET, Chapter 10, Section 10.6
await using var transaction = await db.Database.BeginTransactionAsync();
try
{
    var order = new Order { CustomerId = 1, PlacedAtUtc = DateTime.UtcNow, Total = 100m };
    db.Orders.Add(order);
    await db.SaveChangesAsync();                 // order.Id is now populated

    db.OrderLines.Add(new OrderLine { OrderId = order.Id, Sku = "A1", Quantity = 2, UnitPrice = 50m });
    await db.SaveChangesAsync();

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();           // also happens automatically on dispose without commit
    throw;
}
