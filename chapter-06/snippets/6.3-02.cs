// Modern .NET, Chapter 6, Section 6.3
var work = LongOperationAsync();
var timeout = Task.Delay(TimeSpan.FromSeconds(2));

var winner = await Task.WhenAny(work, timeout);
Console.WriteLine(winner == work ? "Finished in time" : "Timed out");

static async Task LongOperationAsync() => await Task.Delay(500);
