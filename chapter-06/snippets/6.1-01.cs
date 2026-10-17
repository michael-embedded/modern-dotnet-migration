// Modern .NET, Chapter 6, Section 6.1
public class ReportService
{
    public string BuildReport(int customerId)
    {
        var customer = _http.GetString("https://api.example.com/customers/" + customerId); // blocks
        var orders = _db.QueryOrders(customerId);                                          // blocks
        var file = File.ReadAllText(@"C:\templates\report.txt");                           // blocks
        return Render(file, customer, orders);
    }
}
