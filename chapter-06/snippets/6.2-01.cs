// Modern .NET, Chapter 6, Section 6.2
Console.WriteLine($"1. Start on thread {Environment.CurrentManagedThreadId}");

var task = FetchAsync();

Console.WriteLine($"2. FetchAsync returned a task; Main continues on thread {Environment.CurrentManagedThreadId}");

var result = await task;

Console.WriteLine($"4. Got '{result}' on thread {Environment.CurrentManagedThreadId}");

static async Task<string> FetchAsync()
{
    Console.WriteLine($"   FetchAsync starting on thread {Environment.CurrentManagedThreadId}");
    await Task.Delay(300);   // no thread is blocked during this wait
    Console.WriteLine($"3. FetchAsync resumed on thread {Environment.CurrentManagedThreadId}");
    return "data";
}
