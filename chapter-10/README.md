# Chapter 10: Entity Framework Core — Modern Data Access

*Migrating Legacy Data Layers to EF Core Without Losing Control of Your Database*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 10.12): Migrate a Legacy Data Access Layer
- **Build-Along** (Section 10.14): Modernizing an EF6 Application to EF Core

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 10.1 | Legacy ADO.NET and Entity Framework Patterns | [`10.1-01.cs`](snippets/10.1-01.cs) |
| 10.2 | EF6 vs Entity Framework Core | [`10.2-01.sh`](snippets/10.2-01.sh), [`10.2-02.cs`](snippets/10.2-02.cs) |
| 10.3 | Migrating DbContext and Entity Models | [`10.3-01.sh`](snippets/10.3-01.sh), [`10.3-02.cs`](snippets/10.3-02.cs), [`10.3-03.cs`](snippets/10.3-03.cs) |
| 10.4 | Modern LINQ Queries | [`10.4-01.cs`](snippets/10.4-01.cs), [`10.4-02.cs`](snippets/10.4-02.cs), [`10.4-03.cs`](snippets/10.4-03.cs), [`10.4-04.cs`](snippets/10.4-04.cs) |
| 10.5 | Database Migrations and Schema Management | [`10.5-01.sh`](snippets/10.5-01.sh), [`10.5-02.sql`](snippets/10.5-02.sql), [`10.5-03.sh`](snippets/10.5-03.sh), [`10.5-04.sh`](snippets/10.5-04.sh), [`10.5-05.sh`](snippets/10.5-05.sh) |
| 10.6 | Transactions and Concurrency | [`10.6-01.cs`](snippets/10.6-01.cs), [`10.6-02.cs`](snippets/10.6-02.cs), [`10.6-03.cs`](snippets/10.6-03.cs) |
| 10.7 | Query Performance and Indexing | [`10.7-01.cs`](snippets/10.7-01.cs), [`10.7-02.cs`](snippets/10.7-02.cs), [`10.7-03.cs`](snippets/10.7-03.cs), [`10.7-04.cs`](snippets/10.7-04.cs) |
| 10.8 | Raw SQL and Stored Procedures | [`10.8-01.cs`](snippets/10.8-01.cs), [`10.8-02.cs`](snippets/10.8-02.cs), [`10.8-03.cs`](snippets/10.8-03.cs) |
| 10.10 | Before & After: Legacy Data Access vs EF Core | [`10.10-01.cs`](snippets/10.10-01.cs), [`10.10-02.cs`](snippets/10.10-02.cs) |
| 10.11 | Common Mistakes: N+1 Queries, Tracking Overhead, and Poor LINQ | [`10.11-01.cs`](snippets/10.11-01.cs) |
| 10.12 | Mini Project: Migrate a Legacy Data Access Layer | [`10.12-01.sh`](snippets/10.12-01.sh), [`10.12-02.cs`](snippets/10.12-02.cs), [`10.12-03.cs`](snippets/10.12-03.cs), [`10.12-04.cs`](snippets/10.12-04.cs) |
| 10.13 | Performance Benchmark: EF Core Queries and Database Access Patterns | [`10.13-01.sh`](snippets/10.13-01.sh), [`10.13-02.cs`](snippets/10.13-02.cs) |
| 10.14 | Build-Along: Modernizing an EF6 Application to EF Core | [`10.14-01.sh`](snippets/10.14-01.sh), [`10.14-02.cs`](snippets/10.14-02.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
