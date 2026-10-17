// Modern .NET, Chapter 10, Section 10.10
public List<OrderRow> GetPaidOrders(int customerId)
{
    using (var db = new ShopContext())                    // new per call, hidden connection string
    {
        var orders = db.Orders.Where(o => o.CustomerId == customerId && o.Status == 1).ToList();
        return orders.Select(o => new OrderRow
        {
            Id = o.Id,
            Customer = o.Customer.Name,                   // lazy load: 1 query per order
            LineCount = o.Lines.Count,                    // lazy load: another query per order
            Total = o.Total
        }).ToList();
    }
}
