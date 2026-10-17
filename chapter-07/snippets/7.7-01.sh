# Modern .NET, Chapter 7, Section 7.7
dotnet list package --outdated
dotnet add src/MyLib/MyLib.csproj package Newtonsoft.Json --version 13.0.3
dotnet build
dotnet test
