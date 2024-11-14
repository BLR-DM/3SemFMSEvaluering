using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FMSUserService.Domain;
using FMSUserService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your token as 'Bearer {your_token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<UserManager<AppUser>>();
//builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDbContext<FMSIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FMSIdentityDbConnection")));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false; // HUSK <-!
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 2;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    //options.User.AllowedUserNameCharacters =
    //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});


builder.Services
    .AddIdentityApiEndpoints<AppUser>() //Gude linje
    .AddEntityFrameworkStores<FMSIdentityContext>(); //Gude linje

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.Headers.Add("Authentication-Failed", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                // Log token validation success
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Token validated successfully for user: {User}", context.Principal.Identity.Name);
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Tester", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Test");
    });

    options.AddPolicy("klasse1", policy =>
    {
        //logik her 

    });
});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGroup("/account").MapIdentityApi<AppUser>();

app.MapPost("/api/login",
    async (LoginRequest loginRequest, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IConfiguration configuration) =>
    {
        var user = await userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
        if (!result.Succeeded)
        {
            return Results.Unauthorized();
        }

        
        var token = GenerateJwtToken(user, configuration);
        return Results.Ok(new { token });
    });

string GenerateJwtToken(AppUser user, IConfiguration configuration)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.Role, "Test"),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapGet("/test", () => "Hello World").RequireAuthorization("Tester");


app.MapPost("/classes", async (FMSIdentityContext context, string userId) =>
{
    //var appUser = await userManager.FindByIdAsync(userId);
    //if (appUser == null)
    //{
    //    return Results.NotFound("User not found");
    //}

    await context.Classes.AddAsync(new Class(userId));
    await context.SaveChangesAsync();
    return Results.Ok();

});

app.Run();


