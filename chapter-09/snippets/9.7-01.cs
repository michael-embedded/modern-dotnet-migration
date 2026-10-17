// Modern .NET, Chapter 9, Section 9.7
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

// DEMO ONLY: in real life this key comes from a secret store, and the issuer is your identity provider.
const string demoKey = "this-is-a-demo-signing-key-at-least-32-bytes-long!!";
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(demoKey));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,   ValidIssuer = "https://shop.example/issuer",
            ValidateAudience = true, ValidAudience = "shop-api",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ProductWriter", p => p.RequireClaim("scope", "products.write"));
