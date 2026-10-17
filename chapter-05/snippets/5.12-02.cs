// Modern .NET, Chapter 5, Section 5.12
public static class LegacyInvoiceService
{
    public static string CreateInvoice(string customer, decimal amount)
    {
        var tax = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["TaxRate"] ?? "0.1");
        var total = amount + amount * tax;
        var id = "INV-" + DateTime.Now.Ticks;
        Console.WriteLine("Created " + id);
        return id + ":" + customer + ":" + total;
    }
}
