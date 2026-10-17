# Chapter 9: Modern APIs — Building Better ASP.NET Core Services

*REST, Minimal APIs, Authentication, OpenAPI, and Production-Ready Backend Services*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 9.12): Build a Production-Ready .NET 10 API
- **Build-Along** (Section 9.14): Modernizing a Legacy REST API

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 9.2 | Controllers vs Minimal APIs | [`9.2-01.cs`](snippets/9.2-01.cs), [`9.2-02.cs`](snippets/9.2-02.cs), [`9.2-03.cs`](snippets/9.2-03.cs), [`9.2-04.cs`](snippets/9.2-04.cs) |
| 9.3 | Designing Clean REST Endpoints | [`9.3-01.sh`](snippets/9.3-01.sh), [`9.3-02.cs`](snippets/9.3-02.cs) |
| 9.4 | API Versioning and Backward Compatibility | [`9.4-01.cs`](snippets/9.4-01.cs), [`9.4-02.cs`](snippets/9.4-02.cs) |
| 9.5 | OpenAPI and API Documentation | [`9.5-01.cs`](snippets/9.5-01.cs), [`9.5-02.cs`](snippets/9.5-02.cs), [`9.5-03.cs`](snippets/9.5-03.cs) |
| 9.6 | Problem Details and Consistent Error Handling | [`9.6-01.cs`](snippets/9.6-01.cs), [`9.6-02.cs`](snippets/9.6-02.cs), [`9.6-03.cs`](snippets/9.6-03.cs) |
| 9.7 | Authentication, Authorization, and JWTs | [`9.7-01.cs`](snippets/9.7-01.cs), [`9.7-02.cs`](snippets/9.7-02.cs), [`9.7-03.sh`](snippets/9.7-03.sh) |
| 9.8 | Rate Limiting and Resilience | [`9.8-01.cs`](snippets/9.8-01.cs), [`9.8-02.cs`](snippets/9.8-02.cs) |
| 9.10 | Before & After: Legacy Web API vs Modern ASP.NET Core API | [`9.10-01.cs`](snippets/9.10-01.cs), [`9.10-02.cs`](snippets/9.10-02.cs) |
| 9.12 | Mini Project: Build a Production-Ready .NET 10 API | [`9.12-01.cs`](snippets/9.12-01.cs) |
| 9.13 | Performance Benchmark: Controllers vs Minimal APIs | [`9.13-01.sh`](snippets/9.13-01.sh), [`9.13-02.cs`](snippets/9.13-02.cs) |
| 9.14 | Build-Along: Modernizing a Legacy REST API | [`9.14-01.cs`](snippets/9.14-01.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
