// Modern .NET, Chapter 6, Section 6.3
try
{
    await SlowAsync().WaitAsync(TimeSpan.FromMilliseconds(200));
}
catch (TimeoutException)
{
    Console.WriteLine("Operation timed out.");
}

static async Task SlowAsync() => await Task.Delay(2000);
