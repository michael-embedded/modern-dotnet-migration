// Modern .NET, Chapter 7, Section 7.4
using System.Text.Json;
using System.Text.Json.Serialization;

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)   // camelCase, case-insensitive
{
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() },
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

var order = new Order(
    Id: 7,
    Status: OrderStatus.Shipped,
    Notes: null,
    Payment: new CardPayment("4242", 59.90m));

var json = JsonSerializer.Serialize(order, options);
Console.WriteLine(json);

var roundTrip = JsonSerializer.Deserialize<Order>(json, options)!;
Console.WriteLine($"Round trip equal: {roundTrip.Payment is CardPayment { LastFour: "4242" }}");

// Tolerant input: unknown property, different casing, status as text
const string incoming = """{ "ID": 8, "status": "Pending", "extra": true, "payment": { "kind": "invoice", "terms": 30, "amount": 100 } }""";
var parsed = JsonSerializer.Deserialize<Order>(incoming, options)!;
Console.WriteLine($"{parsed.Id} {parsed.Status} {parsed.Payment}");

public enum OrderStatus { Pending, Shipped }

[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(CardPayment), "card")]
[JsonDerivedType(typeof(InvoicePayment), "invoice")]
public abstract record Payment(decimal Amount);

public sealed record CardPayment(string LastFour, decimal Amount) : Payment(Amount);
public sealed record InvoicePayment(int Terms, decimal Amount) : Payment(Amount);

public sealed record Order(int Id, OrderStatus Status, string? Notes, Payment Payment);
