// Modern .NET, Chapter 7, Section 7.8
using System.Globalization;

public class CultureBehaviorTests
{
    [Fact]
    public void Currency_FormatsConsistently()
    {
        var culture = CultureInfo.GetCultureInfo("de-DE");
        Assert.Equal("1.234,50 €", 1234.5m.ToString("C", culture).Replace('\u00A0', ' '));
    }

    [Fact]
    public void Sorting_IsExplicitlyOrdinal()
    {
        var items = new[] { "b", "A", "a", "B" };
        var sorted = items.OrderBy(x => x, StringComparer.Ordinal).ToArray();
        Assert.Equal(["A", "B", "a", "b"], sorted);
    }
}
