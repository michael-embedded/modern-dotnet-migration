# Modern .NET, Chapter 11, Section 11.7
dotnet tool install --global dotnet-counters
dotnet-counters monitor --process-id <PID> --counters Shop.Orders,Microsoft.AspNetCore.Hosting
