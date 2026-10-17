// Modern .NET, Chapter 4, Section 4.4
static string Classify(int[] values) => values switch
{
    []                      => "empty",
    [var single]            => $"one item: {single}",
    [var first, .., var last] => $"from {first} to {last}",
};
