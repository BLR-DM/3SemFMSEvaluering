using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FMSDataServer.Api;
using FMSDataServer.Api.ModelDto;
using FMSDataServer.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContext<FMSDataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
    .AddEntityFrameworkStores<FMSDataDbContext>(); //Gude linje
                                                   //.AddDefaultTokenProviders();  


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       //options.Events = new JwtBearerEvents
       //{
       //    OnAuthenticationFailed = context =>
       //    {
       //        context.Response.Headers.Add("Authentication-Failed", context.Exception.Message);
       //        return Task.CompletedTask;
       //    },
       //    OnTokenValidated = context =>
       //    {
       //        // Log token validation success
       //        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
       //        logger.LogInformation("Token validated successfully for user: {User}", context.Principal.Identity.Name);
       //        return Task.CompletedTask;
       //    }
       //};

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

builder.Services.AddAuthorization();

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



app.MapPost("/fms/register",
    async (UserManager<AppUser> _userManager, RegisterDto registerDto, FMSDataDbContext _context) =>
    {
        var student = await _context.Students.SingleOrDefaultAsync(e => e.Email == registerDto.Email);
        if (student == null)
        {
            return Results.BadRequest("Student with this email doesnt exist");
        }

        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return Results.BadRequest("Password and confirmation password doesnt match");
        }

        var user = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }

        student.AppUser = user;
        await _context.SaveChangesAsync();

        return Results.Ok(new { Message = "User registered" });
    });

app.MapPost("/fms/login", async (UserManager<AppUser> _userManager, LoginDto loginDto, IConfiguration _configuration, FMSDataDbContext _context) =>
{
    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
    {
        return Results.Unauthorized();
    }

    var student = await _context.Students
        .Include(s => s.Class)
        .SingleAsync(s => s.AppUser.Id == user.Id);

    var token = GenerateJwtToken(user, _configuration, student);

    return Results.Ok(new { Token = token });
}).AllowAnonymous();

app.MapGet("/fms/helloworld", (HttpContext httpContext) =>
{
    if (httpContext.User.Identity.IsAuthenticated)
    {
        return Results.Ok();
    }

    return Results.Unauthorized();
});

string GenerateJwtToken(AppUser user, IConfiguration configuration, Student student)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("class", student.Class.Name),
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


app.MapPost("/fms/subject", async (FMSDataDbContext dbContext, Subject subject) =>
{
    dbContext.Subjects.Add(subject);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/Subject/{subject.Id}", subject);
});

app.MapPost("/fms/teacher", async (FMSDataDbContext dbContext, Teacher teacher) =>
{
    dbContext.Teachers.Add(teacher);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/teacher/{teacher.Id}", teacher);
});


app.MapPost("/fms/class", async (FMSDataDbContext dbContext, ModelClass modelClass) =>
{
    dbContext.Classes.Add(modelClass);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/class/{modelClass.Id}", modelClass);
});

app.MapPost("/fms/student", async (FMSDataDbContext dbContext, Student student) =>
{
    dbContext.Students.Add(student);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/student/{student.Id}", student);
});


app.MapPost("/fms/teachersubject", async (FMSDataDbContext dbContext, TeacherSubject teacherSubject) =>
{
    dbContext.TeacherSubjects.Add(teacherSubject);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/teachersubject/{teacherSubject.Id}", teacherSubject);
});

app.MapPost("/fms/lecture", async (FMSDataDbContext dbContext, Lecture lecture) =>
{
    dbContext.Lectures.Add(lecture);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/lecture/{lecture.Id}", lecture);
});


app.MapGet("/fms/subject", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Subjects.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/teacher", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Teachers.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/class", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Classes.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/student", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Students.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/student/{appUserId}", async (string appUserId, FMSDataDbContext _context) =>
{
    var student = await _context.Students
        .AsNoTracking()
        .Where(s => s.AppUser.Id == appUserId)
        .Select(s => new StudentDto
        {
            Id = s.Id.ToString(),
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            ClassId = s.Class.Id.ToString(),
            AppUserId = s.AppUser.Id
        })
        .SingleOrDefaultAsync();

    if (student == null)
    {
        return Results.NotFound($"Student with AppUserId {appUserId} not found.");
    }

    return Results.Ok(student);
});

app.MapGet("/fms/teachersubject", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.TeacherSubjects.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/lecture", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Lectures.AsNoTracking().ToListAsync());
});





app.Run();

