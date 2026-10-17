# Chapter 13: Containers, CI/CD & Cloud-Native .NET

*Docker, Deployment Pipelines, Observability, and .NET Aspire for Modern Distributed Applications*

**Level:** Advanced

## Projects in this chapter

- **Mini Project** (Section 13.13): Containerize and Deploy a .NET 10 API

## Code listings by section

Every code listing from the chapter is saved in [`snippets/`](snippets), named by section number and order of appearance.

| Section | Title | Files |
|---|---|---|
| 13.2 | Containerizing a Modern .NET Application | [`13.2-01.sh`](snippets/13.2-01.sh), [`13.2-02.cs`](snippets/13.2-02.cs), [`13.2-03.sh`](snippets/13.2-03.sh), [`13.2-04.xml`](snippets/13.2-04.xml), [`13.2-05.sh`](snippets/13.2-05.sh), [`13.2-06.Dockerfile`](snippets/13.2-06.Dockerfile), [`13.2-07.txt`](snippets/13.2-07.txt), [`13.2-08.sh`](snippets/13.2-08.sh) |
| 13.3 | Building Efficient .NET Docker Images | [`13.3-01.Dockerfile`](snippets/13.3-01.Dockerfile), [`13.3-02.xml`](snippets/13.3-02.xml), [`13.3-03.sh`](snippets/13.3-03.sh) |
| 13.4 | Configuration and Secrets in Containers | [`13.4-01.cs`](snippets/13.4-01.cs), [`13.4-02.yml`](snippets/13.4-02.yml), [`13.4-03.sh`](snippets/13.4-03.sh), [`13.4-04.cs`](snippets/13.4-04.cs) |
| 13.5 | CI/CD With GitHub Actions | [`13.5-01.yml`](snippets/13.5-01.yml) |
| 13.6 | Automated Testing and Deployment Gates | [`13.6-01.cs`](snippets/13.6-01.cs) |
| 13.7 | Health Checks and Production Readiness | [`13.7-01.yml`](snippets/13.7-01.yml) |
| 13.8 | Introduction to .NET Aspire | [`13.8-01.sh`](snippets/13.8-01.sh), [`13.8-02.cs`](snippets/13.8-02.cs) |
| 13.9 | Service Discovery and Distributed Application Development | [`13.9-01.cs`](snippets/13.9-01.cs), [`13.9-02.cs`](snippets/13.9-02.cs), [`13.9-03.cs`](snippets/13.9-03.cs) |
| 13.11 | Before & After: Manual Deployment vs Automated Cloud Deployment | [`13.11-01.txt`](snippets/13.11-01.txt), [`13.11-02.yml`](snippets/13.11-02.yml), [`13.11-03.conf`](snippets/13.11-03.conf), [`13.11-04.sh`](snippets/13.11-04.sh) |

## Notes

- These are the listings exactly as they appear in the book, one file per listing. Many are excerpts, so they are not complete projects on their own.
- Listings marked `LEGACY` are deliberately old code. Listings marked `MODERN` show what it becomes. Anything broken on purpose says so in a comment.
- To try a listing, create a project with the .NET 10 SDK (`dotnet new console`, `dotnet new web`, or `dotnet new classlib`) and paste the code in.
