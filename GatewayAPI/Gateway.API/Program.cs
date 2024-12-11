using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    // Tillad anonym adgang til login endpoint
    if (path != null && path.StartsWith("/fmsdataserver/login", StringComparison.OrdinalIgnoreCase) || path != null && path.StartsWith("/fmsdataserver/register", StringComparison.OrdinalIgnoreCase))
    {
        await next.Invoke();
    }
    else
    {
        // Tving autentifikation for alle andre proxied ruter
        var authResult = await context.RequestServices.GetRequiredService<IAuthenticationService>()
            .AuthenticateAsync(context, JwtBearerDefaults.AuthenticationScheme);

        if (!authResult.Succeeded)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next.Invoke();
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapReverseProxy();
app.Run();