# Modern .NET, Chapter 13, Section 13.8
dotnet new install Aspire.ProjectTemplates      # one-time; skip if templates are already available
dotnet new aspire-starter -n ShopHost
cd ShopHost
dotnet run --project ShopHost.AppHost
