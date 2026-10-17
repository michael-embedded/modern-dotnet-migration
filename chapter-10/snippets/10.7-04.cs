// Modern .NET, Chapter 10, Section 10.7
var page = await db.Orders
    .Where(o => o.Id > lastSeenId)
    .OrderBy(o => o.Id)
    .Take(20)
    .ToListAsync();
