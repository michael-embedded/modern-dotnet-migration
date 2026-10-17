// Modern .NET, Chapter 4, Section 4.3
public enum OrderStatus { Pending, Paid, Shipped, Cancelled }

public sealed record Order(int Id, decimal Total, OrderStatus Status);
