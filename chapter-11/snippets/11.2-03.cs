// Modern .NET, Chapter 11, Section 11.2
// tests/Billing.Tests/InvoiceCharacterizationTests.cs
using Billing;

namespace Billing.Tests;

public class InvoiceCharacterizationTests
{
    [Theory]
    [InlineData(0, "US", false, 0)]
    [InlineData(100.005, "US", false, 107.01)]       // banker's rounding at work: recorded, not "fixed"
    [InlineData(1000.01, "DE", true, 1048.84)]
    [InlineData(5000, "GB", true, 5520.00)]
    [InlineData(10, "FR", false, 10)]
    [InlineData(10, null, true, 9.50)]
    public void Total_MatchesRecordedLegacyBehavior(decimal subtotal, string? country, bool member, decimal expected) =>
        Assert.Equal(expected, LegacyInvoiceCalculator.Total(subtotal, country, member));
}
