# Chapter 6: Async & Concurrency — Modernizing Application Work

*From Blocking Calls to Scalable Asynchronous .NET Applications*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 6.12): Convert a Synchronous Service to Async
- **Build-Along** (Section 6.14): Building a Concurrent Background Processing Pipeline

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 6.1 | What's Wrong With Synchronous Application Code | [`6.1-01.cs`](snippets/6.1-01.cs), [`6.1-02.cs`](snippets/6.1-02.cs) |
| 6.2 | Understanding async and await | [`6.2-01.cs`](snippets/6.2-01.cs), [`6.2-02.cs`](snippets/6.2-02.cs), [`6.2-03.cs`](snippets/6.2-03.cs) |
| 6.3 | Task, ValueTask, and Asynchronous APIs | [`6.3-01.cs`](snippets/6.3-01.cs), [`6.3-02.cs`](snippets/6.3-02.cs), [`6.3-03.cs`](snippets/6.3-03.cs), [`6.3-04.cs`](snippets/6.3-04.cs) |
| 6.4 | Cancellation Tokens and Cooperative Cancellation | [`6.4-01.cs`](snippets/6.4-01.cs), [`6.4-02.cs`](snippets/6.4-02.cs) |
| 6.5 | Parallel Programming in Modern .NET | [`6.5-01.cs`](snippets/6.5-01.cs), [`6.5-02.cs`](snippets/6.5-02.cs), [`6.5-03.cs`](snippets/6.5-03.cs) |
| 6.6 | Channels and Producer-Consumer Workloads | [`6.6-01.cs`](snippets/6.6-01.cs) |
| 6.7 | Thread Safety and Synchronization | [`6.7-01.cs`](snippets/6.7-01.cs), [`6.7-02.cs`](snippets/6.7-02.cs) |
| 6.8 | Migrating Blocking Database and HTTP Calls | [`6.8-01.cs`](snippets/6.8-01.cs), [`6.8-02.sh`](snippets/6.8-02.sh), [`6.8-03.cs`](snippets/6.8-03.cs) |
| 6.10 | Before & After: Synchronous Workflows vs Async Pipelines | [`6.10-01.cs`](snippets/6.10-01.cs) |
| 6.11 | Common Mistakes: Async Over Sync and Blocking on Tasks | [`6.11-01.cs`](snippets/6.11-01.cs) |
| 6.12 | Mini Project: Convert a Synchronous Service to Async | [`6.12-01.sh`](snippets/6.12-01.sh), [`6.12-02.cs`](snippets/6.12-02.cs) |
| 6.13 | Performance Benchmark: Blocking vs Async Application Workloads | [`6.13-01.sh`](snippets/6.13-01.sh), [`6.13-02.cs`](snippets/6.13-02.cs) |
| 6.14 | Build-Along: Building a Concurrent Background Processing Pipeline | [`6.14-01.sh`](snippets/6.14-01.sh), [`6.14-02.cs`](snippets/6.14-02.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
