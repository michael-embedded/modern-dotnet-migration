// Modern .NET, Chapter 4, Section 4.7
var text = "Modern .NET Migration";
Console.WriteLine(text[..6]);        // "Modern"
Console.WriteLine(text[^9..]);       // "Migration"

var items = new[] { 10, 20, 30, 40, 50 };
Console.WriteLine(string.Join(",", items[1..^1]));  // 20,30,40
