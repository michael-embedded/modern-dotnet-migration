// Modern .NET, Chapter 9, Section 9.5
builder.Services.AddOpenApi();            // before Build()

// after Build():
app.MapOpenApi();                          // serves /openapi/v1.json
