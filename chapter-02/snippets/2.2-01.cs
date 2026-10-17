// Modern .NET, Chapter 2, Section 2.2
// Add as a new file, ReferenceAudit.cs, in the MigrationInventory project
// and call ReferenceAudit.Run(root) from Program.cs before 'return 0;'
using System.Xml.Linq;

public static class ReferenceAudit
{
    private static readonly Dictionary<string, string> Watchlist = new(StringComparer.OrdinalIgnoreCase)
    {
        ["System.Web"]                     = "Classic ASP.NET. Needs ASP.NET Core migration (Chapter 8).",
        ["System.Web.Mvc"]                 = "ASP.NET MVC 5. See Chapter 8.",
        ["System.ServiceModel"]            = "WCF. Client: CoreWCF/WCF client packages. Server: consider gRPC or REST.",
        ["System.Runtime.Remoting"]        = ".NET Remoting is not supported. Replace.",
        ["System.EnterpriseServices"]      = "COM+ services. No direct equivalent.",
        ["System.Data.OracleClient"]       = "Removed from the Framework. Use Oracle's own provider.",
        ["System.Configuration"]           = "web.config/app.config APIs. See Chapter 5.",
        ["System.Drawing"]                 = "Windows-centric on legacy. Check cross-platform needs.",
        ["System.Management"]              = "WMI. Windows-only.",
        ["Microsoft.Office.Interop.Excel"] = "COM interop. Windows + Office required.",
    };

    public static void Run(string root)
    {
        Console.WriteLine();
        Console.WriteLine("== Reference watchlist ==");

        foreach (var path in Directory.EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories))
        {
            var doc = XDocument.Load(path);
            var references = doc.Descendants()
                .Where(e => e.Name.LocalName == "Reference")
                .Select(e => (e.Attribute("Include")?.Value ?? "").Split(',')[0].Trim())
                .Where(n => n.Length > 0);

            foreach (var reference in references)
            {
                if (Watchlist.TryGetValue(reference, out var advice))
                {
                    Console.WriteLine($"{Path.GetFileNameWithoutExtension(path),-30} {reference,-34} {advice}");
                }
            }
        }
    }
}
