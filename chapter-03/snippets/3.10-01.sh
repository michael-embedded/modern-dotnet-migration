# Modern .NET, Chapter 3, Section 3.10
mkdir Demo && cd Demo
dotnet new sln -n Demo
dotnet new classlib -n Demo.Core -o src/Demo.Core
dotnet new xunit -n Demo.Tests -o tests/Demo.Tests
dotnet sln add src/Demo.Core tests/Demo.Tests
dotnet add tests/Demo.Tests reference src/Demo.Core
dotnet test
