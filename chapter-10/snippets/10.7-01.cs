// Modern .NET, Chapter 10, Section 10.7
var query = db.Orders
    .Where(o => o.CustomerId == 1 && o.Status == OrderStatus.Paid)
    .OrderByDescending(o => o.PlacedAtUtc)
    .Take(10);

Console.WriteLine(query.ToQueryString());     // prints the SQL EF Core will run
