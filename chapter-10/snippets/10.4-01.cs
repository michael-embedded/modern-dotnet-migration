// Modern .NET, Chapter 10, Section 10.4
// Throws: the provider cannot translate a custom C# method to SQL
var bad = await db.Customers.Where(c => IsVip(c.Name)).ToListAsync();

// Fix: express it in translatable terms, or pull data first deliberately
var good = await db.Customers.Where(c => c.Name.StartsWith("VIP")).ToListAsync();
var deliberate = (await db.Customers.ToListAsync()).Where(c => IsVip(c.Name)).ToList();  // explicit, visible cost

static bool IsVip(string name) => name.Contains("VIP");
