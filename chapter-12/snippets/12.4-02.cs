// Modern .NET, Chapter 12, Section 12.4
using System.Globalization;

var line = "42,Widget,19.99";

if (TryParse(line, out var id, out var name, out var price))
    Console.WriteLine($"{id} | {name} | {price}");

static bool TryParse(ReadOnlySpan<char> line, out int id, out string name, out decimal price)
{
    id = 0; name = ""; price = 0;

    var first = line.IndexOf(',');
    if (first < 0) return false;
    var rest = line[(first + 1)..];
    var second = rest.IndexOf(',');
    if (second < 0) return false;

    return int.TryParse(line[..first], CultureInfo.InvariantCulture, out id)
        && decimal.TryParse(rest[(second + 1)..], CultureInfo.InvariantCulture, out price)
        && (name = rest[..second].ToString()) is not null;     // allocate only the one string we keep
}
