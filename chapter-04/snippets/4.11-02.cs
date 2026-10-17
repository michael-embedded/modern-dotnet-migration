// Modern .NET, Chapter 4, Section 4.11
using System;
using System.Collections.Generic;

namespace Discounts
{
    public class LineItem
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class LegacyDiscountService
    {
        public decimal Calculate(List<LineItem> items, string customerTier)
        {
            if (items == null || items.Count == 0) return 0;

            decimal subtotal = 0;
            foreach (var item in items)
            {
                subtotal += item.Quantity * item.UnitPrice;
            }

            decimal rate;
            if (customerTier == null) rate = 0;
            else if (customerTier.ToLower() == "gold") rate = 0.15m;
            else if (customerTier.ToLower() == "silver") rate = 0.10m;
            else if (customerTier.ToLower() == "bronze") rate = 0.05m;
            else rate = 0;

            if (subtotal > 500) rate += 0.02m;

            return Math.Round(subtotal * rate, 2);
        }
    }
}
