// Modern .NET, Chapter 6, Section 6.2
public sealed class ReportService(HttpClient http, IOrderRepository orders)
{
    public async Task<string> BuildReportAsync(int customerId, CancellationToken ct = default)
    {
        var customerTask = http.GetStringAsync($"https://api.example.com/customers/{customerId}", ct);
        var ordersTask = orders.GetOrdersAsync(customerId, ct);
        var templateTask = File.ReadAllTextAsync("report.txt", ct);

        // The three I/O operations run concurrently; total time ~ the slowest, not the sum.
        await Task.WhenAll(customerTask, ordersTask, templateTask);

        return Render(templateTask.Result, customerTask.Result, ordersTask.Result);
    }

    private static string Render(string template, string customer, IReadOnlyList<Order> orders) =>
        $"{template}\n{customer}\n{orders.Count} orders";
}
