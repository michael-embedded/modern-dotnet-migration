// Modern .NET, Chapter 10, Section 10.7
e.HasIndex(o => new { o.CustomerId, o.Status, o.PlacedAtUtc })
 .HasDatabaseName("IX_Orders_Customer_Status_Placed");
