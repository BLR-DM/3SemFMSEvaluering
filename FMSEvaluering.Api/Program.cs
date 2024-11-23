using System.Security.Claims;
using FMSEvaluering.Application;
using FMSEvaluering.Application.Authorization;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreate", policy =>
        policy.RequireClaim("class", "DVME231"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapPost("/post",
//    async (CreatePostDto post, IPostCommand command) => await command.CreatePostAsync(post))
//    .RequireAuthorization("CanCreate");

app.MapPost("/post",
    async (CreatePostDto post, IAuthorizationService authService, ClaimsPrincipal user, IPostCommand command) =>
    {
        var requirement = new ClassroomAccessRequirement(post.ClassId);
        var result = await authService.AuthorizeAsync(user, null, requirement);

        if (!result.Succeeded)
        {
            return Results.Forbid();
        }

        await command.CreatePostAsync(post);

        return Results.Created();
    });

app.MapPut("/post",
    async (UpdatePostDto postHistory, IPostCommand command) => await command.AddPostHistory(postHistory));

app.MapGet("/post/{id}",
    async (int id, IPostQuery postQuery) => await postQuery.GetPostAsync(id));

app.MapDelete("/post", 
    async ([FromBody] DeletePostDto post, IPostCommand command) => await command.DeletePostAsync(post));
//.RequireAuthorization("isAdmin");


//VOTE
//VOTE
//VOTE
app.MapPost("/post/vote",
    async (CreateVoteDto dto, IPostCommand command) =>
        await command.CreateVote(dto));
app.MapPut("/post/vote",
    async (UpdateVoteDto dto, IPostCommand command) =>
        await command.UpdateVote(dto));
app.MapDelete("/post/vote",
    async ([FromBody]DeleteVoteDto dto, IPostCommand command) =>
        await command.DeleteVote(dto));

//Comment
//Comment
//Comment

app.MapPost("/post/comment",
    async (CreateCommentDto dto, IPostCommand command) =>
        await command.CreateCommentAsync(dto));
app.MapPut("/post/comment",
    async (UpdateCommentDto dto, IPostCommand command) =>
        await command.UpdateCommentAsync(dto));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();