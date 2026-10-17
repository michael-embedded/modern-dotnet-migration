# Modern .NET, Chapter 13, Section 13.3
COPY global.json Directory.Build.props Directory.Packages.props ./
COPY src/Shop.Api/Shop.Api.csproj src/Shop.Api/
COPY src/Shop.Core/Shop.Core.csproj src/Shop.Core/
RUN dotnet restore src/Shop.Api/Shop.Api.csproj
COPY src/ src/
RUN dotnet publish src/Shop.Api/Shop.Api.csproj -c Release -o /app/publish --no-restore
