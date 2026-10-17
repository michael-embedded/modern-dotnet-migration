# Chapter 2: Auditing Your Legacy .NET Application

*How to Assess, Measure, and Plan a Migration Without Breaking Production*

**Level:** Beginner

## Projects in this chapter

- **Mini Project** (Section 2.11): Build a .NET Migration Audit Report

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 2.1 | Taking Inventory of Your Application | [`2.1-01.sh`](snippets/2.1-01.sh), [`2.1-02.sh`](snippets/2.1-02.sh), [`2.1-03.cs`](snippets/2.1-03.cs), [`2.1-04.sh`](snippets/2.1-04.sh) |
| 2.2 | Identifying Framework, Runtime, and SDK Dependencies | [`2.2-01.cs`](snippets/2.2-01.cs) |
| 2.3 | Auditing NuGet Packages and Third-Party Libraries | [`2.3-01.sh`](snippets/2.3-01.sh), [`2.3-02.cs`](snippets/2.3-02.cs) |
| 2.4 | Identifying High-Risk vs Low-Risk Components | [`2.4-01.cs`](snippets/2.4-01.cs) |
| 2.5 | Finding Deprecated APIs and Unsupported Technologies | [`2.5-01.xml`](snippets/2.5-01.xml), [`2.5-02.ps1`](snippets/2.5-02.ps1) |
| 2.7 | Building a Rollback Strategy | [`2.7-01.txt`](snippets/2.7-01.txt) |
| 2.9 | Before & After: A Migration Plan That Failed vs One That Succeeded | [`2.9-01.cs`](snippets/2.9-01.cs) |
| 2.11 | Mini Project: Build a .NET Migration Audit Report | [`2.11-01.cs`](snippets/2.11-01.cs), [`2.11-02.cs`](snippets/2.11-02.cs) |
| 2.12 | Performance Benchmark: Measuring the Legacy Application Baseline | [`2.12-01.cs`](snippets/2.12-01.cs), [`2.12-02.sh`](snippets/2.12-02.sh) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
