// Modern .NET, Chapter 3, Section 3.12
using Pricing;

namespace Pricing.Tests;

public class DiscountCalculatorTests
{
    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(19.99, 15, 16.99)]
    [InlineData(50, 0, 50)]
    [InlineData(50, 100, 0)]
    public void Apply_ReturnsExpectedPrice(decimal price, decimal percent, decimal expected)
    {
        Assert.Equal(expected, DiscountCalculator.Apply(price, percent));
    }

    [Fact]
    public void Apply_RejectsNegativePercent()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => DiscountCalculator.Apply(10, -1));
    }
}
