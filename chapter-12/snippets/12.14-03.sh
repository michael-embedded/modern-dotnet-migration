# Modern .NET, Chapter 12, Section 12.14
dotnet run -c Release --project BaselineProbe -- http://localhost:5000/catalog/slow 100 10
dotnet run -c Release --project BaselineProbe -- http://localhost:5000/catalog/fast 100 10
