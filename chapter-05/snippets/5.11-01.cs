// Modern .NET, Chapter 5, Section 5.11
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<RequestState>();
services.AddSingleton<BadCache>();   // singleton depending on scoped: a captive dependency

// ValidateScopes and ValidateOnBuild make the container detect the mistake.
var options = new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true };

try
{
    using var provider = services.BuildServiceProvider(options);
}
catch (Exception ex)
{
    Console.WriteLine("Caught at startup: " + ex.Message);
}

public sealed class RequestState { }
public sealed class BadCache(RequestState state) { public RequestState State { get; } = state; }
