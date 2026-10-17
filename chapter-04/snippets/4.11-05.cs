// Modern .NET, Chapter 4, Section 4.11
// Add to DiscountTests.cs
[Theory]
[MemberData(nameof(Cases))]
public void Modern_MatchesLegacy(List<LineItem>? items, string? tier, decimal expected)
{
    var lines = (items ?? []).Select(i => new Line(i.Sku, i.Quantity, i.UnitPrice)).ToList();
    var actual = DiscountCalculator.Calculate(lines, DiscountCalculator.ParseTier(tier));
    Assert.Equal(expected, actual);
}
