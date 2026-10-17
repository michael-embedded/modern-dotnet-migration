// Modern .NET, Chapter 5, Section 5.12
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["Invoicing:TaxRate"] = "0.10",
    ["Invoicing:Prefix"] = "INV"
});

builder.Services
    .AddOptions<InvoicingOptions>()
    .Bind(builder.Configuration.GetSection("Invoicing"))
    .Validate(o => o.TaxRate is >= 0 and <= 1, "TaxRate must be between 0 and 1")
    .ValidateOnStart();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IInvoiceNumberGenerator, TimestampInvoiceNumberGenerator>();
builder.Services.AddTransient<InvoiceService>();

using var host = builder.Build();

var service = host.Services.GetRequiredService<InvoiceService>();
var invoice = service.Create("Ada Lovelace", 250m);
Console.WriteLine(invoice);

public sealed class InvoicingOptions
{
    public decimal TaxRate { get; set; }
    public string Prefix { get; set; } = "INV";
}

public sealed record Invoice(string Number, string Customer, decimal Net, decimal Total);

public interface IInvoiceNumberGenerator
{
    string Next();
}

public sealed class TimestampInvoiceNumberGenerator(
    IOptions<InvoicingOptions> options, TimeProvider clock) : IInvoiceNumberGenerator
{
    public string Next() => $"{options.Value.Prefix}-{clock.GetUtcNow():yyyyMMddHHmmssfff}";
}

public sealed class InvoiceService(
    IInvoiceNumberGenerator numbers,
    IOptions<InvoicingOptions> options,
    ILogger<InvoiceService> logger)
{
    public Invoice Create(string customer, decimal net)
    {
        var total = Math.Round(net * (1 + options.Value.TaxRate), 2);
        var invoice = new Invoice(numbers.Next(), customer, net, total);
        logger.LogInformation("Created invoice {InvoiceNumber} for {Customer}", invoice.Number, customer);
        return invoice;
    }
}
