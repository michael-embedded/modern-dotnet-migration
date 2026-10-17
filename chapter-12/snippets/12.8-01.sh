# Modern .NET, Chapter 12, Section 12.8
dotnet tool install --global dotnet-counters
dotnet-counters ps
dotnet-counters monitor --process-id <PID> System.Runtime
