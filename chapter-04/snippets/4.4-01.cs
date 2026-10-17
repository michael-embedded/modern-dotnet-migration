// Modern .NET, Chapter 4, Section 4.4
public decimal CalculateShipping(Order order, string country, bool isPremium)
{
    decimal cost;
    if (isPremium)
    {
        cost = 0;
    }
    else if (country == "US")
    {
        if (order.Total > 100) cost = 0;
        else if (order.Total > 50) cost = 4.99m;
        else cost = 9.99m;
    }
    else if (country == "CA" || country == "MX")
    {
        cost = order.Total > 150 ? 0 : 14.99m;
    }
    else
    {
        cost = 29.99m;
    }
    return cost;
}
