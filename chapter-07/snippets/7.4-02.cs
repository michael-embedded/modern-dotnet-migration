// Modern .NET, Chapter 7, Section 7.4
using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
                             WriteIndented = false)]
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(List<Order>))]
public partial class AppJsonContext : JsonSerializerContext;

// Usage:
// var json = JsonSerializer.Serialize(order, AppJsonContext.Default.Order);
// var back = JsonSerializer.Deserialize(json, AppJsonContext.Default.Order);
