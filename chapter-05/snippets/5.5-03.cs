// Modern .NET, Chapter 5, Section 5.5
using var host = builder.Build();
ServiceLocator.Initialize(host.Services);   // bridge for legacy call sites
await host.RunAsync();
