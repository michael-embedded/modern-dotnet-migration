// Modern .NET, Chapter 1, Section 1.4
// Save as Program.cs in a new console project: dotnet new console -n DelayCost
double releasesPerYear = 12;
double manualHoursPerRelease = 14;     // build, package, deploy, smoke test
double engineerHourlyCost = 70;        // fully loaded
double incidentsPerYear = 6;
double hoursPerIncident = 20;
double extraHostingPerYear = 18_000;   // Windows licensing vs. Linux containers

double releaseCost  = releasesPerYear * manualHoursPerRelease * engineerHourlyCost;
double incidentCost = incidentsPerYear * hoursPerIncident * engineerHourlyCost;
double total        = releaseCost + incidentCost + extraHostingPerYear;

Console.WriteLine($"Release overhead : {releaseCost,10:C0}");
Console.WriteLine($"Incident overhead: {incidentCost,10:C0}");
Console.WriteLine($"Hosting premium  : {extraHostingPerYear,10:C0}");
Console.WriteLine(new string('-', 30));
Console.WriteLine($"Annual cost of delay: {total,8:C0}");
