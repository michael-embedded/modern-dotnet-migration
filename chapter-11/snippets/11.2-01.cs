// Modern .NET, Chapter 11, Section 11.2
namespace Billing;

public static class LegacyInvoiceCalculator
{
    public static decimal Total(decimal subtotal, string? country, bool isMember)
    {
        decimal tax = country switch
        {
            "US" => 0.07m,
            "DE" => 0.19m,
            "GB" => 0.20m,
            _ => 0m
        };

        var discount = isMember ? 0.05m : 0m;
        if (subtotal > 1000m) discount += 0.03m;

        var net = subtotal * (1 - discount);
        var total = net * (1 + tax);
        return Math.Round(total, 2);                  // note: banker's rounding (the default)
    }
}
