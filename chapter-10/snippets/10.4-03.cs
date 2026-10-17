// Modern .NET, Chapter 10, Section 10.4
// 1. Eager loading: join in one query
var orders = await db.Orders.Include(o => o.Lines).Where(o => o.CustomerId == 1).ToListAsync();

// 2. Split query: one SQL per collection, avoids the cartesian explosion of big joins
var split = await db.Orders.Include(o => o.Lines).AsSplitQuery().ToListAsync();

// 3. Explicit loading: on demand, when you know you need it
var order = await db.Orders.FirstAsync();
await db.Entry(order).Collection(o => o.Lines).LoadAsync();
