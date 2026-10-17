// Modern .NET, Chapter 7, Section 7.6
// Windows-1252 needs the code-pages provider on modern .NET:
// dotnet add package System.Text.Encoding.CodePages
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var legacyEncoding = System.Text.Encoding.GetEncoding(1252);
