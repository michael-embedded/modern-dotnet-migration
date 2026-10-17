# Modern .NET, Chapter 3, Section 3.12
mkdir ToolchainDemo && cd ToolchainDemo
git init
dotnet new sln -n ToolchainDemo
dotnet new classlib -n Pricing -o src/Pricing
dotnet new xunit -n Pricing.Tests -o tests/Pricing.Tests
dotnet sln add src/Pricing tests/Pricing.Tests
dotnet add tests/Pricing.Tests reference src/Pricing
dotnet new gitignore
