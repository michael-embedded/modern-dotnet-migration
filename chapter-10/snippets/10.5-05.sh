# Modern .NET, Chapter 10, Section 10.5
dotnet new console -n MigrationsDemo
cd MigrationsDemo
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
# add the model + context from this section, then:
dotnet ef migrations add Initial
dotnet ef database update
# change the model (add a property), then:
dotnet ef migrations add AddLoyaltyPoints
dotnet ef migrations script Initial AddLoyaltyPoints
