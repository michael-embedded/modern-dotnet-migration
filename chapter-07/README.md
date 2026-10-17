# Chapter 7: Modernizing Libraries, HTTP & Application Services

*Replacing Legacy APIs, Outdated Communication Patterns, and Fragile Application Services*

**Level:** Intermediate

## Projects in this chapter

- **Mini Project** (Section 7.12): Modernize a Legacy HTTP Integration Service

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 7.1 | Identifying Deprecated .NET APIs | [`7.1-01.xml`](snippets/7.1-01.xml), [`7.1-02.cs`](snippets/7.1-02.cs) |
| 7.2 | Modern HTTP Communication With HttpClient | [`7.2-01.cs`](snippets/7.2-01.cs), [`7.2-02.cs`](snippets/7.2-02.cs) |
| 7.3 | HttpClientFactory and Connection Management | [`7.3-01.sh`](snippets/7.3-01.sh), [`7.3-02.cs`](snippets/7.3-02.cs) |
| 7.4 | Serialization With System.Text.Json | [`7.4-01.cs`](snippets/7.4-01.cs), [`7.4-02.cs`](snippets/7.4-02.cs) |
| 7.5 | Replacing Legacy Configuration APIs | [`7.5-01.xml`](snippets/7.5-01.xml), [`7.5-02.json`](snippets/7.5-02.json), [`7.5-03.cs`](snippets/7.5-03.cs) |
| 7.6 | Modern File, Stream, and Process APIs | [`7.6-01.cs`](snippets/7.6-01.cs), [`7.6-02.cs`](snippets/7.6-02.cs), [`7.6-03.cs`](snippets/7.6-03.cs), [`7.6-04.cs`](snippets/7.6-04.cs), [`7.6-05.cs`](snippets/7.6-05.cs) |
| 7.7 | Updating Third-Party Dependencies | [`7.7-01.sh`](snippets/7.7-01.sh) |
| 7.8 | Handling Breaking API Changes | [`7.8-01.cs`](snippets/7.8-01.cs) |
| 7.10 | Before & After: Legacy HTTP and Serialization vs Modern .NET APIs | [`7.10-01.cs`](snippets/7.10-01.cs), [`7.10-02.cs`](snippets/7.10-02.cs) |
| 7.12 | Mini Project: Modernize a Legacy HTTP Integration Service | [`7.12-01.sh`](snippets/7.12-01.sh), [`7.12-02.cs`](snippets/7.12-02.cs), [`7.12-03.cs`](snippets/7.12-03.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
