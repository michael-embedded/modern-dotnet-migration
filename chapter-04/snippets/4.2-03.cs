// Modern .NET, Chapter 4, Section 4.2
#nullable enable

public static class NameFormatter
{
    public static string DisplayName(string? name, string? email) =>
        !string.IsNullOrWhiteSpace(name) ? name
        : !string.IsNullOrWhiteSpace(email) ? email
        : "Unknown";
}
