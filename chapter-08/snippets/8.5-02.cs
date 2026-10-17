// Modern .NET, Chapter 8, Section 8.5
app.MapGet("/Products/Details/{id:int}", (int id) => Results.RedirectPermanent($"/products/{id}"));
