// Modern .NET, Chapter 5, Section 5.7
foreach (var (key, value) in builder.Configuration.AsEnumerable().Where(kv => kv.Key.StartsWith("Pricing")))
{
    Console.WriteLine($"{key} = {value}");
}
