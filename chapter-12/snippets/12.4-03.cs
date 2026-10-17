// Modern .NET, Chapter 12, Section 12.4
ReadOnlySpan<char> csv = "alpha,beta,gamma";
foreach (var range in csv.Split(','))
    Console.WriteLine(csv[range].ToString());
