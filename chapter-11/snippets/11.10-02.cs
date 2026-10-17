// Modern .NET, Chapter 11, Section 11.10
using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(o => { o.IncludeScopes = true; o.UseUtcTimestamp = true; });

builder.Services.AddSingleton<InvoiceMetrics>();
builder.Services.AddSingleton<InvoiceService>();
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();

builder.Services.AddOpenTelemetry()
    .WithTracing(t => t.AddAspNetCoreInstrumentation().AddSource("Invoices").AddConsoleExporter())
    .WithMetrics(m => m.AddAspNetCoreInstrumentation().AddMeter(InvoiceMetrics.MeterName).AddConsoleExporter());

var app = builder.Build();

app.UseExceptionHandler();
app.MapHealthChecks("/health");

app.MapPost("/invoices", (InvoiceRequest request, InvoiceService service, ILogger<Program> logger) =>
{
    using var scope = logger.BeginScope(new Dictionary<string, object> { ["CustomerId"] = request.CustomerId });
    var invoice = service.Create(request);
    logger.LogInformation("Invoice {InvoiceId} created total {Total}", invoice.Id, invoice.Total);
    return Results.Created($"/invoices/{invoice.Id}", invoice);
});

app.Run();

public sealed record InvoiceRequest(int CustomerId, decimal Amount);
public sealed record Invoice(Guid Id, int CustomerId, decimal Total);

public sealed class InvoiceMetrics
{
    public const string MeterName = "Invoices";
    private readonly Counter<long> _created;
    private readonly Histogram<double> _duration;

    public InvoiceMetrics(IMeterFactory factory)
    {
        var meter = factory.Create(MeterName);
        _created = meter.CreateCounter<long>("invoices.created");
        _duration = meter.CreateHistogram<double>("invoices.duration", unit: "ms");
    }

    public void Record(double ms) { _created.Add(1); _duration.Record(ms); }
}

public sealed class InvoiceService(InvoiceMetrics metrics)
{
    private static readonly ActivitySource Source = new("Invoices");

    public Invoice Create(InvoiceRequest request)
    {
        var start = Stopwatch.GetTimestamp();
        using var activity = Source.StartActivity("CreateInvoice");
        activity?.SetTag("customer.id", request.CustomerId);

        if (request.Amount <= 0) throw new ArgumentException("Amount must be positive.", nameof(request));

        var invoice = new Invoice(Guid.NewGuid(), request.CustomerId, Math.Round(request.Amount * 1.08m, 2));
        metrics.Record(Stopwatch.GetElapsedTime(start).TotalMilliseconds);
        return invoice;
    }
}
