# Modern .NET, Chapter 14, Section 14.13
mkdir MigrationLab && cd MigrationLab
dotnet new sln -n MigrationLab
dotnet new web -n Legacy.App -o src/Legacy.App
dotnet new web -n Modern.App -o src/Modern.App
dotnet new web -n Gateway -o src/Gateway
dotnet new xunit -n Equivalence.Tests -o tests/Equivalence.Tests
dotnet sln add src/Legacy.App src/Modern.App src/Gateway tests/Equivalence.Tests
dotnet add src/Gateway package Yarp.ReverseProxy
dotnet add tests/Equivalence.Tests reference src/Legacy.App src/Modern.App
dotnet add tests/Equivalence.Tests package Microsoft.AspNetCore.Mvc.Testing
