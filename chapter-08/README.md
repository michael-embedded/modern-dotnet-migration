# Chapter 8: ASP.NET Core — Leaving System.Web Behind

*Migrating Legacy ASP.NET Applications to the Modern Web Stack*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 8.12): Migrate a Legacy MVC Controller

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 8.1 | ASP.NET Framework vs ASP.NET Core | [`8.1-01.sh`](snippets/8.1-01.sh), [`8.1-02.cs`](snippets/8.1-02.cs) |
| 8.2 | Understanding the Modern Middleware Pipeline | [`8.2-01.cs`](snippets/8.2-01.cs), [`8.2-02.cs`](snippets/8.2-02.cs), [`8.2-03.cs`](snippets/8.2-03.cs) |
| 8.3 | Replacing Global.asax | [`8.3-01.cs`](snippets/8.3-01.cs), [`8.3-02.cs`](snippets/8.3-02.cs) |
| 8.4 | Migrating MVC Applications | [`8.4-01.cs`](snippets/8.4-01.cs), [`8.4-02.cs`](snippets/8.4-02.cs), [`8.4-03.cshtml`](snippets/8.4-03.cshtml), [`8.4-04.cshtml`](snippets/8.4-04.cshtml), [`8.4-05.cshtml`](snippets/8.4-05.cshtml) |
| 8.5 | Modern Routing and Endpoint Mapping | [`8.5-01.cs`](snippets/8.5-01.cs), [`8.5-02.cs`](snippets/8.5-02.cs) |
| 8.6 | Replacing Legacy Session and State Management | [`8.6-01.cs`](snippets/8.6-01.cs), [`8.6-02.cs`](snippets/8.6-02.cs) |
| 8.7 | Migrating Authentication and Authorization | [`8.7-01.cs`](snippets/8.7-01.cs) |
| 8.8 | Model Binding and Validation in ASP.NET Core | [`8.8-01.cs`](snippets/8.8-01.cs) |
| 8.10 | Before & After: System.Web vs ASP.NET Core | [`8.10-01.cs`](snippets/8.10-01.cs), [`8.10-02.cs`](snippets/8.10-02.cs) |
| 8.12 | Mini Project: Migrate a Legacy MVC Controller | [`8.12-01.sh`](snippets/8.12-01.sh), [`8.12-02.cs`](snippets/8.12-02.cs), [`8.12-03.cs`](snippets/8.12-03.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
