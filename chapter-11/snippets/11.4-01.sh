# Modern .NET, Chapter 11, Section 11.4
dotnet new web -n Store.Api
dotnet new xunit -n Store.Api.Tests
dotnet add Store.Api.Tests reference Store.Api
dotnet add Store.Api.Tests package Microsoft.AspNetCore.Mvc.Testing
