// Modern .NET, Chapter 12, Section 12.4
// Allocates: a string[], plus each substring
var parts = line.Split(',');
var id = int.Parse(parts[0]);
var price = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
