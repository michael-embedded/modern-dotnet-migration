# Modern .NET, Chapter 12, Section 12.8
dotnet tool install --global dotnet-trace
dotnet-trace collect --process-id <PID> --profile cpu-sampling --duration 00:00:30 --format Speedscope
