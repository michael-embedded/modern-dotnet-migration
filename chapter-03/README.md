# Chapter 3: Setting Up Your Modern .NET Toolchain

*SDKs, Project Files, CLI Tools, IDEs, and CI/CD for Modern .NET*

**Level:** Beginner

## Projects in this chapter

- **Mini Project** (Section 3.12): Create a Modern .NET 10 Project With CI/CD

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 3.1 | Installing and Managing .NET SDK Versions | [`3.1-01.ps1`](snippets/3.1-01.ps1), [`3.1-02.sh`](snippets/3.1-02.sh), [`3.1-03.sh`](snippets/3.1-03.sh) |
| 3.2 | Understanding Target Framework Monikers | [`3.2-01.xml`](snippets/3.2-01.xml), [`3.2-02.cs`](snippets/3.2-02.cs) |
| 3.3 | SDK-Style Project Files | [`3.3-01.xml`](snippets/3.3-01.xml), [`3.3-02.xml`](snippets/3.3-02.xml), [`3.3-03.sh`](snippets/3.3-03.sh) |
| 3.4 | Migrating From Legacy .csproj Files | [`3.4-01.xml`](snippets/3.4-01.xml), [`3.4-02.xml`](snippets/3.4-02.xml), [`3.4-03.xml`](snippets/3.4-03.xml), [`3.4-04.cs`](snippets/3.4-04.cs) |
| 3.5 | Replacing packages.config With Modern NuGet Management | [`3.5-01.xml`](snippets/3.5-01.xml), [`3.5-02.xml`](snippets/3.5-02.xml), [`3.5-03.xml`](snippets/3.5-03.xml) |
| 3.6 | Managing SDK Versions With global.json | [`3.6-01.json`](snippets/3.6-01.json), [`3.6-02.sh`](snippets/3.6-02.sh) |
| 3.7 | Static Analysis and Code Quality Tools | [`3.7-01.ini`](snippets/3.7-01.ini), [`3.7-02.cs`](snippets/3.7-02.cs) |
| 3.8 | Setting Up CI/CD for Modern .NET | [`3.8-01.yml`](snippets/3.8-01.yml), [`3.8-02.yml`](snippets/3.8-02.yml), [`3.8-03.yml`](snippets/3.8-03.yml) |
| 3.10 | Before & After: Legacy Project Configuration vs SDK-Style Projects | [`3.10-01.sh`](snippets/3.10-01.sh) |
| 3.12 | Mini Project: Create a Modern .NET 10 Project With CI/CD | [`3.12-01.sh`](snippets/3.12-01.sh), [`3.12-02.cs`](snippets/3.12-02.cs), [`3.12-03.cs`](snippets/3.12-03.cs), [`3.12-04.xml`](snippets/3.12-04.xml), [`3.12-05.sh`](snippets/3.12-05.sh) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
