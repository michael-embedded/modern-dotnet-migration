// Modern .NET, Chapter 13, Section 13.9
builder.Services.AddHttpClient<WeatherClient>(client =>
{
    client.BaseAddress = new Uri("https+http://apiservice");     // resolved via service discovery
});
