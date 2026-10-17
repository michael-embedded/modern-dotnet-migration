// Modern .NET, Chapter 3, Section 3.12
namespace Pricing;

public static class DiscountCalculator
{
    public static decimal Apply(decimal price, decimal percent)
    {
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
        if (percent is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percent));

        return Math.Round(price * (1 - percent / 100m), 2, MidpointRounding.AwayFromZero);
    }
}
