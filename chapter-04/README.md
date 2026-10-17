# Chapter 4: Modern C# — Cleaning Up Legacy Code

*Nullable References, Records, Pattern Matching, and the C# Features That Make Legacy Code Easier to Maintain*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 4.11): Refactor a Legacy C# Service

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 4.1 | What's Wrong With Legacy C# Patterns | [`4.1-01.cs`](snippets/4.1-01.cs) |
| 4.2 | Nullable Reference Types and Safer Code | [`4.2-01.xml`](snippets/4.2-01.xml), [`4.2-02.cs`](snippets/4.2-02.cs), [`4.2-03.cs`](snippets/4.2-03.cs), [`4.2-04.cs`](snippets/4.2-04.cs) |
| 4.3 | Records and Immutable Data Models | [`4.3-01.cs`](snippets/4.3-01.cs), [`4.3-02.cs`](snippets/4.3-02.cs), [`4.3-03.cs`](snippets/4.3-03.cs) |
| 4.4 | Modern Pattern Matching | [`4.4-01.cs`](snippets/4.4-01.cs), [`4.4-02.cs`](snippets/4.4-02.cs), [`4.4-03.cs`](snippets/4.4-03.cs), [`4.4-04.cs`](snippets/4.4-04.cs), [`4.4-05.xml`](snippets/4.4-05.xml) |
| 4.5 | File-Scoped Namespaces and Global Usings | [`4.5-01.cs`](snippets/4.5-01.cs), [`4.5-02.sh`](snippets/4.5-02.sh), [`4.5-03.cs`](snippets/4.5-03.cs) |
| 4.6 | Required Members and Modern Object Construction | [`4.6-01.cs`](snippets/4.6-01.cs), [`4.6-02.cs`](snippets/4.6-02.cs), [`4.6-03.cs`](snippets/4.6-03.cs), [`4.6-04.cs`](snippets/4.6-04.cs) |
| 4.7 | Collection Expressions and Modern Collection Syntax | [`4.7-01.cs`](snippets/4.7-01.cs), [`4.7-02.cs`](snippets/4.7-02.cs), [`4.7-03.cs`](snippets/4.7-03.cs) |
| 4.9 | Before & After: Legacy C# Patterns vs Modern C# | [`4.9-01.cs`](snippets/4.9-01.cs), [`4.9-02.cs`](snippets/4.9-02.cs) |
| 4.11 | Mini Project: Refactor a Legacy C# Service | [`4.11-01.sh`](snippets/4.11-01.sh), [`4.11-02.cs`](snippets/4.11-02.cs), [`4.11-03.cs`](snippets/4.11-03.cs), [`4.11-04.cs`](snippets/4.11-04.cs), [`4.11-05.cs`](snippets/4.11-05.cs) |
| 4.12 | Performance Benchmark: Legacy vs Modern C# Implementations | [`4.12-01.sh`](snippets/4.12-01.sh), [`4.12-02.cs`](snippets/4.12-02.cs), [`4.12-03.sh`](snippets/4.12-03.sh) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
