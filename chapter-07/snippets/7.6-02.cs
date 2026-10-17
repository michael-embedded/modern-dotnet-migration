// Modern .NET, Chapter 7, Section 7.6
static string ResolveSafe(string baseDir, string userFileName)
{
    var full = Path.GetFullPath(Path.Combine(baseDir, userFileName));
    var root = Path.GetFullPath(baseDir) + Path.DirectorySeparatorChar;

    if (!full.StartsWith(root, StringComparison.Ordinal))
        throw new UnauthorizedAccessException("Path escapes the allowed directory.");

    return full;
}
