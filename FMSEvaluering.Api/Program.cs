using System.Security.Claims;
using System.Text;
using FMSEvaluering.Api.Endpoints;
using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Domain.Entities.PostEntities;
using FMSEvaluering.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your token as 'Bearer {your_token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
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
            new string[] { }
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
    options.AddPolicy("CanCreatePost", policy =>
        policy.RequireClaim("class", "DVME231"));

    options.AddPolicy("Student", policy =>
        policy.RequireClaim("usertype", "student"));

    options.AddPolicy("Teacher", policy =>
        policy.RequireClaim("usertype", "teacher"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.MapPost("/post",
//    async (CreatePostDto post, IPostCommand command) => await command.CreatePostAsync(post))
//    .RequireAuthorization("CanCreate");

app.MapPost("/forum/{forumId}/post",
    async (int forumId, CreatePostDto post, ClaimsPrincipal user, IPostCommand command) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await command.CreatePostAsync(post, appUserId, forumId);
            return Results.Created("testURI", post); // Test return value
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't create post");
        }
    }).RequireAuthorization("Student");

app.MapPut("/post/",
    async (UpdatePostDto post, ClaimsPrincipal user, IPostCommand command) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await command.UpdatePost(post, appUserId);
            return Results.Created("testURI", post);
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't update post");
        }
    });

app.MapDelete("/post",
    async ([FromBody] DeletePostDto post, IPostCommand command) => await command.DeletePostAsync(post));
//.RequireAuthorization("isAdmin");

app.MapGet("/forum/post/{id}",
    async (int id, ClaimsPrincipal user, IPostQuery postQuery) =>
    {
        var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await postQuery.GetPostAsync(id, appUserId);
    });

app.MapGet("/teacher", () => "hej med dig teacher").RequireAuthorization("Teacher");

app.MapGet("/student", () => "hej med dig elev").RequireAuthorization("Student");

//VOTE
//VOTE
//VOTE
//app.MapVoteEndpoints();

app.MapPost("/post/{postId}/vote",
    async (int postId, CreateVoteDto voteDto, ClaimsPrincipal user, IPostCommand command) =>
    {
        // var id = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
        var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            await command.HandleVote(voteDto, appUserId, postId);
            return Results.Ok("Vote registered");
        }
        catch (Exception)
        {
            return Results.BadRequest("Failed to register the vote");
        }
    }).WithTags("Vote");

//Comment
//Comment
//Comment

app.MapPost("/post/comment",
    async (CreateCommentDto dto, IPostCommand command) =>
        await command.CreateCommentAsync(dto));
app.MapPut("/post/comment",
    async (UpdateCommentDto dto, IPostCommand command) =>
        await command.UpdateCommentAsync(dto));

app.MapForumEndpoints();

app.Run();