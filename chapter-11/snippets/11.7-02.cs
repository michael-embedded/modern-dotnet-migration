// Modern .NET, Chapter 11, Section 11.7
using System.Diagnostics.Metrics;

public sealed class OrderMetrics
{
    public const string MeterName = "Shop.Orders";

    private readonly Counter<long> _created;
    private readonly Histogram<double> _processingMs;

    public OrderMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _created = meter.CreateCounter<long>("orders.created", unit: "{order}", description: "Orders created");
        _processingMs = meter.CreateHistogram<double>("orders.processing.duration", unit: "ms", description: "Order processing time");
    }

    public void OrderCreated(string channel) => _created.Add(1, new KeyValuePair<string, object?>("channel", channel));
    public void Processed(double ms) => _processingMs.Record(ms);
}
