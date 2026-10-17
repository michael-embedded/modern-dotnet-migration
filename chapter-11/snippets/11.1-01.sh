# Modern .NET, Chapter 11, Section 11.1
dotnet new sln -n SafetyNet
dotnet new classlib -n Billing -o src/Billing
dotnet new xunit -n Billing.Tests -o tests/Billing.Tests
dotnet sln add src/Billing tests/Billing.Tests
dotnet add tests/Billing.Tests reference src/Billing
