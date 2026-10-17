// Modern .NET, Chapter 4, Section 4.4
public static string Describe(object? value) => value switch
{
    null                          => "nothing",
    int n when n < 0              => $"negative integer {n}",
    int n                         => $"integer {n}",
    string { Length: 0 }          => "empty string",
    string s                      => $"string of length {s.Length}",
    IEnumerable<object> items     => $"collection with {items.Count()} items",
    Order { Status: OrderStatus.Cancelled } => "a cancelled order",
    Order o                       => $"order #{o.Id}",
    _                             => $"something else: {value.GetType().Name}"
};
