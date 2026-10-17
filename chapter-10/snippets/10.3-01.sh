# Modern .NET, Chapter 10, Section 10.3
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef dbcontext scaffold "Data Source=shop.db" Microsoft.EntityFrameworkCore.Sqlite \
  --output-dir Data/Entities --context ShopContext --context-dir Data --data-annotations --force
