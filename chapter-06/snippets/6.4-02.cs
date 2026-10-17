// Modern .NET, Chapter 6, Section 6.4
public static async Task<string> GetWithTimeoutAsync(
    HttpClient http, string url, TimeSpan timeout, CancellationToken callerToken)
{
    using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(callerToken);
    timeoutCts.CancelAfter(timeout);

    try
    {
        return await http.GetStringAsync(url, timeoutCts.Token);
    }
    catch (OperationCanceledException) when (!callerToken.IsCancellationRequested)
    {
        throw new TimeoutException($"Request to {url} exceeded {timeout.TotalSeconds}s.");
    }
}
