# Modern .NET, Chapter 3, Section 3.1
dotnet new list                       # available project templates
dotnet new sln -n MyApp               # create a solution
dotnet sln add src/MyApp/MyApp.csproj # add a project to a solution
dotnet restore                        # restore NuGet packages
dotnet build -c Release               # compile
dotnet test                           # run tests
dotnet format                         # apply code style rules
