// Modern .NET, Chapter 11, Section 11.2
// tests/Billing.Tests/GoldenMasterGenerator.cs
using Xunit.Abstractions;

namespace Billing.Tests;

public class GoldenMasterGenerator(ITestOutputHelper output)
{
    [Fact(Skip = "Run manually to regenerate expectations")]
    public void Print()
    {
        foreach (var subtotal in new[] { 0m, 10m, 99.99m, 100.005m, 999.99m, 1000m, 1000.01m, 5000m })
        foreach (var country in new string?[] { "US", "DE", "GB", "FR", null })
        foreach (var member in new[] { false, true })
            output.WriteLine($"[InlineData({subtotal}, {(country is null ? "null" : $"\"{country}\"")}, {member.ToString().ToLower()}, {LegacyInvoiceCalculator.Total(subtotal, country, member)})]");
    }
}
