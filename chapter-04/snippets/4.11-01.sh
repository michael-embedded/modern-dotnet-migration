# Modern .NET, Chapter 4, Section 4.11
dotnet new sln -n DiscountRefactor
dotnet new classlib -n Discounts -o src/Discounts
dotnet new xunit -n Discounts.Tests -o tests/Discounts.Tests
dotnet sln add src/Discounts tests/Discounts.Tests
dotnet add tests/Discounts.Tests reference src/Discounts
