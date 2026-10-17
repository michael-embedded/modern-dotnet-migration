# Modern .NET, Chapter 2, Section 2.3
# Packages with newer versions available
dotnet list package --outdated

# Packages with known security vulnerabilities (include transitive dependencies)
dotnet list package --vulnerable --include-transitive

# Packages that have been deprecated by their authors
dotnet list package --deprecated
