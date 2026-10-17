// Modern .NET, Chapter 7, Section 7.6
// Legacy: breaks on Linux
var legacyPath = baseDir + "\\reports\\" + fileName;

// Modern
var path = Path.Combine(baseDir, "reports", fileName);
