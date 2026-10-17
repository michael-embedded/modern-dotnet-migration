// Modern .NET, Chapter 10, Section 10.4
public sealed record OrderSummary(int Id, string CustomerName, decimal Total, int LineCount);

var summaries = await db.Orders
    .AsNoTracking()
    .Where(o => o.Status == OrderStatus.Paid)
    .OrderByDescending(o => o.PlacedAtUtc)
    .Select(o => new OrderSummary(o.Id, o.Customer.Name, o.Total, o.Lines.Count))
    .Take(50)
    .ToListAsync();
