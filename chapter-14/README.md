# Chapter 14: The Full .NET Migration Playbook

*A Step-by-Step Strategy to Take a Legacy .NET Framework Application to .NET 10 in Production*

**Level:** Advanced

## Projects in this chapter

- **Build-Along** (Section 14.13): Migrating a Complete Legacy Application

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 14.1 | The Migration Roadmap — Phase by Phase | [`14.1-01.cs`](snippets/14.1-01.cs) |
| 14.2 | Preparing the Legacy Application | [`14.2-01.cs`](snippets/14.2-01.cs) |
| 14.3 | Converting the Project and Dependency Structure | [`14.3-01.cs`](snippets/14.3-01.cs) |
| 14.4 | Modernizing C# and Application Architecture | [`14.4-01.cs`](snippets/14.4-01.cs) |
| 14.8 | Testing and Catching Regressions Early | [`14.8-01.cs`](snippets/14.8-01.cs) |
| 14.9 | Performance Benchmarking the Migrated Application | [`14.9-01.cs`](snippets/14.9-01.cs) |
| 14.10 | Containerizing and Deploying the Modern Application | [`14.10-01.txt`](snippets/14.10-01.txt) |
| 14.13 | Final Build-Along: Migrating a Complete Legacy Application | [`14.13-01.sh`](snippets/14.13-01.sh), [`14.13-02.cs`](snippets/14.13-02.cs), [`14.13-03.cs`](snippets/14.13-03.cs), [`14.13-04.cs`](snippets/14.13-04.cs), [`14.13-05.json`](snippets/14.13-05.json), [`14.13-06.sh`](snippets/14.13-06.sh), [`14.13-07.cs`](snippets/14.13-07.cs) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
