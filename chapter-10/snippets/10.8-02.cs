// Modern .NET, Chapter 10, Section 10.8
public sealed record CustomerRevenue(string Name, decimal Revenue);

var rows = await db.Database.SqlQuery<CustomerRevenue>($"""
    SELECT c.Name AS Name, SUM(o.Total) AS Revenue
    FROM Customers c JOIN Orders o ON o.CustomerId = c.Id
    GROUP BY c.Name
    ORDER BY Revenue DESC
    """).ToListAsync();
