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



app.MapPost("/register",
    async (UserManager<AppUser> _userManager, RegisterDto registerDto, FMSDataDbContext _context) =>
    {
        var user = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var student = await _context.Students.SingleOrDefaultAsync(e => e.Email == registerDto.Email);
        if (student != null)
        {
            student.AppUser = user;
        }
        else
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Email == registerDto.Email);
            if (teacher != null)
            {
                teacher.AppUser = user;
            }
            else
            {
                return Results.BadRequest("No user with this email exist");
            }
        }

        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return Results.BadRequest("Password and confirmation password doesnt match");
        }


        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }

        await _context.SaveChangesAsync();

        return Results.Ok(new { Message = "User registered" });
    });

app.MapPost("/login", async (UserManager<AppUser> _userManager, LoginDto loginDto, IConfiguration _configuration, FMSDataDbContext _context) =>
{
    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
    {
        return Results.Unauthorized();
    }

    var token = "";

    var student = await _context.Students
        .Include(s => s.Class)
        .SingleOrDefaultAsync(s => s.AppUser.Id == user.Id);
    if (student != null)
    {
        token = GenerateJwtToken(user, _configuration, student);
    }
    else
    {
        var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.AppUser.Id == user.Id);
        if (teacher != null)
        {
            token = GenerateJwtTokenTeacher(user, _configuration, teacher);
        }
        else
        {
            return Results.Unauthorized();
        }
    }

    return Results.Ok(new { Token = token });
}).AllowAnonymous();


string GenerateJwtToken(AppUser user, IConfiguration configuration, Student student)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, student.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, student.LastName),
        new Claim("usertype", "student"),
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

string GenerateJwtTokenTeacher(AppUser user, IConfiguration configuration, Teacher teacher)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, teacher.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, teacher.LastName),
        new Claim("usertype", "teacher"),
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


app.MapGet("/student/{appUserId}", async (string appUserId, FMSDataDbContext _context) =>
{
    var student = await _context.Students
        .AsNoTracking()
        .Include(s => s.AppUser)
        .Include(s => s.Class)
        .ThenInclude(c => c.TeacherSubjects)
        .ThenInclude(ts => ts.Lectures)
        .SingleOrDefaultAsync(s => s.AppUser.Id == appUserId);

    if (student == null)
    {
        return Results.NotFound($"Student with AppUserId {appUserId} not found.");
    }

    var studentDto = new StudentDto
    {
        FirstName = student.FirstName,
        LastName = student.LastName,
        Email = student.Email,
        Class = new ModelClassDto
        {
            Id = student.Class.Id.ToString(),
            Name = student.Class.Name,
            TeacherSubjects = student.Class.TeacherSubjects.Select(ts => new TeacherSubjectDto
            {
                Id = ts.Id.ToString(),
                Lectures = ts.Lectures.Select(l => new LectureDto
                {
                    Id = l.Id.ToString(),
                    Title = l.Title,
                    Date = l.Date
                }).ToList(),
            }).ToList()
        },
        AppUserId = student.AppUser.Id
    };

    return Results.Ok(studentDto);
});

app.MapGet("/teacher/{appUserId}", async (string appUserId, FMSDataDbContext _context) =>
{
    var teacher = await _context.Teachers
        .AsNoTracking()
        .Where(t => t.AppUser.Id == appUserId)
        .Include(t => t.TeacherSubjects)
        .ThenInclude(ts => ts.Subject)
        .Include(t => t.TeacherSubjects)
        .ThenInclude(ts => ts.Class)
        .Select(t => new TeacherDto
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            TeacherSubjects = t.TeacherSubjects.Select(ts => new TeacherSubjectDto
            {
                Id = ts.Id.ToString(),
                Class = new ModelClassDto
                {
                    Id = ts.Class.Id.ToString(),
                    Name = ts.Class.Name
                }
            }).ToList(),
            AppUserId = t.AppUser.Id
        })
        .SingleOrDefaultAsync();

    if (teacher == null)
    {
        return Results.NotFound($"Teacher with AppUserId {appUserId} not found.");
    }
    return Results.Ok(teacher);
});

app.MapGet("/teachersubject/{teacherSubjectId}/teacher", async (string teacherSubjectId, FMSDataDbContext _context) =>
{
    var teacher = await _context.Teachers
        .AsNoTracking()
        .Where(t => t.TeacherSubjects.Any(ts => ts.Id.ToString().Equals(teacherSubjectId)))
        .Select(t => new TeacherDto
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
        })
        .SingleOrDefaultAsync();

    if (teacher == null)
    {
        return Results.NotFound($"Teacher for teachersubject {teacherSubjectId} not found.");
    }
    return Results.Ok(teacher);
});

app.MapGet("/class/{classId}/teachers", async (string classId, FMSDataDbContext _context) =>
{
    var teachers = await _context.Teachers
        .AsNoTracking()
        .Where(t => t.TeacherSubjects.Any(ts => ts.Class.Id.ToString().Equals(classId)))
        .Select(t => new TeacherDto
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
        })
        .ToListAsync();

    if (teachers == null)
    {
        return Results.NotFound($"No teachers for class {classId} not found.");
    }
    return Results.Ok(teachers);
});

app.MapGet("/teachers", async (FMSDataDbContext _context) =>
{
    var teachers = await _context.Teachers
        .AsNoTracking()
        .Select(t => new TeacherDto
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
        })
        .ToListAsync();

    if (teachers == null)
    {
        return Results.NotFound($"No teachers found.");
    }
    return Results.Ok(teachers);
});

app.MapGet("/lecture/{lectureId}", async (int lectureId, FMSDataDbContext _context) =>
{
    var lecture = await _context.Lectures
        .AsNoTracking()
        .Include(l => l.TeacherSubject)
        .ThenInclude(ts => ts.Teacher)
        .Include(l => l.TeacherSubject)
        .ThenInclude(ts => ts.Class)
        .ThenInclude(c => c.Students)
        .Select(l => new LectureDto
        {
            Id = l.Id.ToString(),
            Title = l.Title,
            TeacherSubject = new TeacherSubjectDto
            {
                Id = l.TeacherSubject.Id.ToString(),
                Teacher = new TeacherDto
                {
                    AppUserId = l.TeacherSubject.Teacher.AppUser.Id
                },
                Class = new ModelClassDto
                {
                    Students = l.TeacherSubject.Class.Students.Select(s => new StudentDto
                    {
                        AppUserId = s.AppUser.Id
                    }).ToList()
                }
            }
        })
        .SingleOrDefaultAsync(l => l.Id == lectureId.ToString());


    if (lecture == null)
    {
        return Results.NotFound("Lecture not found");
    }

    return Results.Ok(lecture);
});





app.Run();

