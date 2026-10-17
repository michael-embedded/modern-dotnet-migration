// Modern .NET, Chapter 10, Section 10.7
options.UseSqlite("Data Source=shop.db")
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging()          // development only: includes parameter values
       .EnableDetailedErrors();
