# Modern .NET: Migrating Legacy .NET Framework Applications to .NET 10 and C# 14

Companion code for the book *Modern .NET* by David Lane.

This repository holds every code listing from the book, organized by chapter, so you can copy, run, and adapt the examples without retyping them.

## What is in here

Each chapter has its own directory:

- `README.md`: the chapter title and level, its Mini Project and Build-Along (if it has them), and a table that maps every listing to its section in the book.
- `snippets/`: one file per code listing, named by section number and order of appearance (for example `4.3-02.cs` is the second listing in Section 4.3).

The repository root also holds a `global.json` that pins the .NET 10 SDK, the same file the book builds in Chapter 3.

## Chapters

| Ch. | Directory | Title | Level |
|---|---|---|---|
| 1 | [chapter-01](chapter-01) | The .NET Evolution Story | Beginner |
| 2 | [chapter-02](chapter-02) | Auditing Your Legacy .NET Application | Beginner |
| 3 | [chapter-03](chapter-03) | Setting Up Your Modern .NET Toolchain | Beginner |
| 4 | [chapter-04](chapter-04) | Modern C# — Cleaning Up Legacy Code | Intermediate |
| 5 | [chapter-05](chapter-05) | Dependency Injection & Configuration | Intermediate |
| 6 | [chapter-06](chapter-06) | Async & Concurrency — Modernizing Application Work | Intermediate |
| 7 | [chapter-07](chapter-07) | Modernizing Libraries, HTTP & Application Services | Intermediate |
| 8 | [chapter-08](chapter-08) | ASP.NET Core — Leaving System.Web Behind | Intermediate |
| 9 | [chapter-09](chapter-09) | Modern APIs — Building Better ASP.NET Core Services | Intermediate |
| 10 | [chapter-10](chapter-10) | Entity Framework Core — Modern Data Access | Intermediate |
| 11 | [chapter-11](chapter-11) | Testing, Observability & Reliability | Intermediate |
| 12 | [chapter-12](chapter-12) | High-Performance .NET | Advanced |
| 13 | [chapter-13](chapter-13) | Containers, CI/CD & Cloud-Native .NET | Advanced |
| 14 | [chapter-14](chapter-14) | The Full .NET Migration Playbook | Advanced |

## How to use the snippets

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download). Run `dotnet --version` to confirm it.
2. Open the chapter you are reading and find the listing you want in its table.
3. Create a project to hold it, for example `dotnet new console -n Demo`, and paste the code in. Where a listing needs a package, the book's text names it.

A few things to know:

- **Listings are excerpts.** The book shows the lines that matter, so many listings are not complete programs on their own.
- **Legacy code is labeled.** Listings marked `LEGACY` are deliberately old. Listings marked `MODERN` show what they become. Anything broken on purpose says so in a comment.
- **The book is the source of truth.** If a listing and the book ever disagree, the book's text explains the intent.

## Reporting problems and suggesting improvements

If you find a bug or a better way to write a listing, please open an issue or send a pull request. Say which chapter and section it affects.

## License

All code in this repository is released under the [MIT License](LICENSE). Use it, modify it, and ship it.
