# Modern .NET, Chapter 8, Section 8.12
dotnet new xunit -n ShopMvc.Tests
cd ShopMvc.Tests
dotnet add reference ../ShopMvc/ShopMvc.csproj
dotnet add package Microsoft.AspNetCore.Mvc.Testing
