// Modern .NET, Chapter 4, Section 4.3
#nullable enable

public sealed record Customer(string Name, string Email)
{
    public IReadOnlyList<Order> Orders { get; init; } = [];

    public string DisplayName =>
        !string.IsNullOrWhiteSpace(Name) ? Name :
        !string.IsNullOrWhiteSpace(Email) ? Email : "Unknown";
}
