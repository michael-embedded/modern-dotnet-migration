// Modern .NET, Chapter 4, Section 4.9
// LEGACY
using System;
using System.Collections.Generic;

namespace LegacyShop
{
    public class OrderSummary
    {
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }

    public class OrderReport
    {
        public List<OrderSummary> BuildReport(List<OrderSummary> orders)
        {
            var result = new List<OrderSummary>();
            if (orders == null) return result;

            foreach (var o in orders)
            {
                if (o != null && o.Status != null && o.Status.ToLower() == "paid" && o.Total > 0)
                {
                    result.Add(o);
                }
            }
            result.Sort(delegate (OrderSummary a, OrderSummary b)
            {
                return b.Total.CompareTo(a.Total);
            });
            return result;
        }

        public string Describe(OrderSummary o)
        {
            string label;
            if (o.Total >= 1000) label = "large";
            else if (o.Total >= 100) label = "medium";
            else label = "small";
            return string.Format("{0}: {1:C} ({2})", o.CustomerName, o.Total, label);
        }
    }
}
