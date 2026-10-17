# Chapter 11: Testing, Observability & Reliability

*Building the Safety Net That Makes Large Migrations Possible*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 11.12): Add Tests and Health Checks to a Legacy Service

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 11.1 | Why Tests Matter Before You Migrate | [`11.1-01.sh`](snippets/11.1-01.sh) |
| 11.2 | Characterization Tests for Legacy Applications | [`11.2-01.cs`](snippets/11.2-01.cs), [`11.2-02.cs`](snippets/11.2-02.cs), [`11.2-03.cs`](snippets/11.2-03.cs), [`11.2-04.cs`](snippets/11.2-04.cs) |
| 11.3 | Unit Testing Legacy Business Logic | [`11.3-01.cs`](snippets/11.3-01.cs), [`11.3-02.cs`](snippets/11.3-02.cs), [`11.3-03.cs`](snippets/11.3-03.cs) |
| 11.4 | Integration Testing ASP.NET Core | [`11.4-01.sh`](snippets/11.4-01.sh), [`11.4-02.cs`](snippets/11.4-02.cs), [`11.4-03.cs`](snippets/11.4-03.cs), [`11.4-04.cs`](snippets/11.4-04.cs) |
| 11.5 | Database and API Integration Tests | [`11.5-01.sh`](snippets/11.5-01.sh), [`11.5-02.cs`](snippets/11.5-02.cs) |
| 11.6 | Structured Logging With Modern .NET | [`11.6-01.cs`](snippets/11.6-01.cs) |
| 11.7 | Metrics and Application Health Checks | [`11.7-01.cs`](snippets/11.7-01.cs), [`11.7-02.cs`](snippets/11.7-02.cs), [`11.7-03.sh`](snippets/11.7-03.sh) |
| 11.8 | Distributed Tracing With OpenTelemetry | [`11.8-01.sh`](snippets/11.8-01.sh), [`11.8-02.cs`](snippets/11.8-02.cs), [`11.8-03.sh`](snippets/11.8-03.sh) |
| 11.10 | Before & After: Blind Production Monitoring vs Observable Applications | [`11.10-01.cs`](snippets/11.10-01.cs), [`11.10-02.cs`](snippets/11.10-02.cs) |
| 11.12 | Mini Project: Add Tests and Health Checks to a Legacy Service | [`11.12-01.cs`](snippets/11.12-01.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
