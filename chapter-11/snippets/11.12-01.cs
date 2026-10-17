// Modern .NET, Chapter 11, Section 11.12
public static class Config { public static decimal HappyHourDiscount = 0.20m; }

public class PricingEngine
{
    public decimal PriceFor(string item, decimal basePrice)
    {
        var hour = DateTime.Now.Hour;
        var discount = hour is >= 16 and < 18 ? Config.HappyHourDiscount : 0m;
        var surcharge = item.StartsWith("IMPORT") ? 1.10m : 1m;
        return Math.Round(basePrice * surcharge * (1 - discount), 2);
    }
}
