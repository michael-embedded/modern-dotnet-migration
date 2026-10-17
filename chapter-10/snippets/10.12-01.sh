# Modern .NET, Chapter 10, Section 10.12
dotnet new sln -n DataMigration
dotnet new classlib -n Data -o src/Data
dotnet new xunit -n Data.Tests -o tests/Data.Tests
dotnet sln add src/Data tests/Data.Tests
dotnet add tests/Data.Tests reference src/Data
dotnet add src/Data package Microsoft.EntityFrameworkCore.Sqlite
dotnet add src/Data package Microsoft.Data.Sqlite
