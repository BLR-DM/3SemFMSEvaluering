using System.Security.Claims;
using System.Text;
using FMSExitSlip.Api.Endpoints;
using FMSExitSlip.Application;
using FMSExitSlip.Application.Commands.CommandDto.QuestionDto;
using FMSExitSlip.Application.Commands.CommandDto.ResponseDto;
using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
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
    options.AddPolicy("Student", policy =>
        policy.RequireClaim("usertype", "student"));

    options.AddPolicy("Teacher", policy =>
        policy.RequireClaim("usertype", "teacher"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapExitSlipEndpoints();

app.MapPost("/exitslip/question",
    async (CreateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
    {
        var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            await command.AddQuestionAsync(questionDto, appUserId);
            return Results.Ok("Question added");
        }
        catch (Exception)
        {
            return Results.BadRequest("Failed to add the question");
            throw;
        }
    }).RequireAuthorization("Teacher").WithTags("Questions");

app.MapPut("/exitslip/{id}/question",
    async (int id, UpdateQuestionDto questionDto, ClaimsPrincipal user, IExitSlipCommand command) =>
    {
        await command.UpdateQuestionAsync(questionDto, id);
    }).RequireAuthorization("Teacher").WithTags("Questions");

app.MapDelete("/exitslip/{id}/question", 
    async ([FromBody] DeleteQuestionDto questionDto, IExitSlipCommand command, int id) =>
    {
        await command.DeleteQuestionAsync(questionDto, id);
    });

app.MapPost("/exitslip/{id}/question/response",
    async (int id, CreateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
    {
        //var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

        await command.CreateResponseAsync(responseDto, id);
    }).RequireAuthorization("Student").WithTags("Responses");

app.MapPut("/exitslip/{id}/question/response",
    async (int id, UpdateResponseDto responseDto, ClaimsPrincipal user, IExitSlipCommand command) =>
    {
        //var appUserId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

        await command.UpdateResponseAsync(responseDto, id);
    }).RequireAuthorization("Student").WithTags("Responses");

app.Run();

