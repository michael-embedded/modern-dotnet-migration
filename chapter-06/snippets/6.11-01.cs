// Modern .NET, Chapter 6, Section 6.11
// In a UI or classic ASP.NET context, this can hang forever:
public string GetData()
{
    return GetDataAsync().Result;    // blocks the context thread
}

private async Task<string> GetDataAsync()
{
    await Task.Delay(100);           // continuation wants the blocked context
    return "data";
}
