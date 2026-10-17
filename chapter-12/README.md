# Chapter 12: High-Performance .NET

*Memory, Allocation, Garbage Collection, Async Performance, and Finding the Code That Actually Costs You*

**Level:** Advanced

## Projects in this chapter

- **Mini Project** (Section 12.12): Find and Remove an Allocation Hotspot
- **Build-Along** (Section 12.14): Performance-Tuning a Production-Style API

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 12.1 | Where .NET Applications Actually Lose Performance | [`12.1-01.txt`](snippets/12.1-01.txt) |
| 12.2 | Understanding Garbage Collection | [`12.2-01.cs`](snippets/12.2-01.cs), [`12.2-02.xml`](snippets/12.2-02.xml) |
| 12.3 | Allocations and Memory Pressure | [`12.3-01.cs`](snippets/12.3-01.cs) |
| 12.4 | Span<T>, Memory<T>, and Allocation-Aware Code | [`12.4-01.cs`](snippets/12.4-01.cs), [`12.4-02.cs`](snippets/12.4-02.cs), [`12.4-03.cs`](snippets/12.4-03.cs), [`12.4-04.cs`](snippets/12.4-04.cs) |
| 12.5 | Object Pooling and Reuse | [`12.5-01.cs`](snippets/12.5-01.cs), [`12.5-02.sh`](snippets/12.5-02.sh), [`12.5-03.cs`](snippets/12.5-03.cs) |
| 12.6 | Async Performance and Thread Pool Behavior | [`12.6-01.cs`](snippets/12.6-01.cs), [`12.6-02.cs`](snippets/12.6-02.cs), [`12.6-03.cs`](snippets/12.6-03.cs) |
| 12.7 | Benchmarking With BenchmarkDotNet | [`12.7-01.sh`](snippets/12.7-01.sh), [`12.7-02.cs`](snippets/12.7-02.cs), [`12.7-03.sh`](snippets/12.7-03.sh), [`12.7-04.xml`](snippets/12.7-04.xml), [`12.7-05.cs`](snippets/12.7-05.cs) |
| 12.8 | Profiling CPU and Memory Hotspots | [`12.8-01.sh`](snippets/12.8-01.sh), [`12.8-02.sh`](snippets/12.8-02.sh), [`12.8-03.sh`](snippets/12.8-03.sh), [`12.8-04.cs`](snippets/12.8-04.cs) |
| 12.10 | Before & After: Allocation-Heavy Code vs Performance-Aware .NET | [`12.10-01.cs`](snippets/12.10-01.cs) |
| 12.12 | Mini Project: Find and Remove an Allocation Hotspot | [`12.12-01.sh`](snippets/12.12-01.sh), [`12.12-02.cs`](snippets/12.12-02.cs), [`12.12-03.cs`](snippets/12.12-03.cs), [`12.12-04.cs`](snippets/12.12-04.cs) |
| 12.13 | Performance Benchmark: Legacy vs Optimized .NET Code | [`12.13-01.sh`](snippets/12.13-01.sh), [`12.13-02.xml`](snippets/12.13-02.xml), [`12.13-03.cs`](snippets/12.13-03.cs) |
| 12.14 | Build-Along: Performance-Tuning a Production-Style API | [`12.14-01.sh`](snippets/12.14-01.sh), [`12.14-02.cs`](snippets/12.14-02.cs), [`12.14-03.sh`](snippets/12.14-03.sh) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
