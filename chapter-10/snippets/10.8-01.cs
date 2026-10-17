// Modern .NET, Chapter 10, Section 10.8
var minTotal = 100m;
var big = await db.Orders
    .FromSql($"SELECT * FROM Orders WHERE Total >= {minTotal}")
    .AsNoTracking()
    .ToListAsync();
