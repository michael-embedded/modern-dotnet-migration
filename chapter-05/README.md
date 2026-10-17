# Chapter 5: Dependency Injection & Configuration

*Breaking Static Dependencies and Building Applications That Can Actually Be Tested*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 5.12): Refactor a Legacy Service Layer With DI

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 5.1 | The Problem With Static Dependencies | [`5.1-01.cs`](snippets/5.1-01.cs), [`5.1-02.cs`](snippets/5.1-02.cs) |
| 5.2 | Understanding Built-In .NET Dependency Injection | [`5.2-01.sh`](snippets/5.2-01.sh), [`5.2-02.cs`](snippets/5.2-02.cs), [`5.2-03.cs`](snippets/5.2-03.cs) |
| 5.3 | Service Lifetimes — Singleton, Scoped, and Transient | [`5.3-01.cs`](snippets/5.3-01.cs) |
| 5.4 | Constructor Injection and Composition | [`5.4-01.cs`](snippets/5.4-01.cs), [`5.4-02.cs`](snippets/5.4-02.cs), [`5.4-03.cs`](snippets/5.4-03.cs), [`5.4-04.cs`](snippets/5.4-04.cs) |
| 5.5 | Migrating Service Locator Patterns | [`5.5-01.cs`](snippets/5.5-01.cs), [`5.5-02.cs`](snippets/5.5-02.cs), [`5.5-03.cs`](snippets/5.5-03.cs), [`5.5-04.cs`](snippets/5.5-04.cs) |
| 5.6 | Configuration and the Options Pattern | [`5.6-01.json`](snippets/5.6-01.json), [`5.6-02.cs`](snippets/5.6-02.cs) |
| 5.7 | Environment-Specific Configuration | [`5.7-01.json`](snippets/5.7-01.json), [`5.7-02.sh`](snippets/5.7-02.sh), [`5.7-03.cs`](snippets/5.7-03.cs), [`5.7-04.sh`](snippets/5.7-04.sh), [`5.7-05.sh`](snippets/5.7-05.sh), [`5.7-06.cs`](snippets/5.7-06.cs) |
| 5.8 | Logging and Structured Application Diagnostics | [`5.8-01.cs`](snippets/5.8-01.cs), [`5.8-02.cs`](snippets/5.8-02.cs), [`5.8-03.json`](snippets/5.8-03.json), [`5.8-04.cs`](snippets/5.8-04.cs) |
| 5.10 | Before & After: Static Services vs Dependency Injection | [`5.10-01.cs`](snippets/5.10-01.cs), [`5.10-02.cs`](snippets/5.10-02.cs) |
| 5.11 | Common Mistakes: Captive Dependencies and Incorrect Lifetimes | [`5.11-01.cs`](snippets/5.11-01.cs), [`5.11-02.cs`](snippets/5.11-02.cs) |
| 5.12 | Mini Project: Refactor a Legacy Service Layer With DI | [`5.12-01.sh`](snippets/5.12-01.sh), [`5.12-02.cs`](snippets/5.12-02.cs), [`5.12-03.cs`](snippets/5.12-03.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
