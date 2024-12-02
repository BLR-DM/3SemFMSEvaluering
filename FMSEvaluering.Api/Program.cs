using System.Security.Claims;
using System.Text;
using FMSEvaluering.Api.Endpoints;
using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
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

app.MapPost("/forum/{forumId}/post",
    async (int forumId, CreatePostDto post, ClaimsPrincipal user, IForumCommand command) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = user.FindFirst("usertype")?.Value;
            await command.CreatePostAsync(post, appUserId, role, forumId);
            return Results.Created("testURI", post); // Test return value
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't create post");
        }
    }).RequireAuthorization("Student");

app.MapPut("/forum/{forumId}/post",
    async (int forumId, UpdatePostDto post, ClaimsPrincipal user, IForumCommand command) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = user.FindFirst("usertype")?.Value;
            await command.UpdatePostAsync(post, appUserId, role, forumId);
            return Results.Created("testURI", post);
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't update post", statusCode:500); // test
        }
    }).RequireAuthorization("Student");

app.MapDelete("/forum/{forumId}/post",
    async (int forumId, [FromBody] DeletePostDto post, ClaimsPrincipal user, IForumCommand command) =>
    {
        var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var role = user.FindFirst("usertype")?.Value;
        await command.DeletePostAsync(post, appUserId, role, forumId);
    });
//.RequireAuthorization("isAdmin");

app.MapGet("/forum/post/{postId}",
    async (int postId, ClaimsPrincipal user, IPostQuery postQuery) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = user.FindFirst("usertype")?.Value;
            var post = await postQuery.GetPostAsync(postId, appUserId, role);
            return Results.Ok(post);
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't get post");
        }
    });

app.MapGet("/forum/{forumId}/post",
    async (int forumId, ClaimsPrincipal user, IPostQuery postQuery) =>
    {
        try
        {
            var appUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = user.FindFirst("usertype")?.Value;
            var posts = await postQuery.GetPostsAsync(forumId, appUserId, role);
            return Results.Ok(posts);
        }
        catch (Exception)
        {
            return Results.Problem("Couldn't get posts");
        }
    });

//VOTE
//VOTE
//VOTE
//app.MapVoteEndpoints();

app.MapPost("/post/{postId}/vote",
    async (int postId, HandleVoteDto voteDto, ClaimsPrincipal user, IPostCommand command) =>
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