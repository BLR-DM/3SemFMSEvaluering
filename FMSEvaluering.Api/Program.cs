using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands.CommandDto.CommentDto;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Queries.Interfaces;
using FMSEvaluering.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/post",
    async (CreatePostDto post, IPostCommand command) => await command.CreatePostAsync(post));

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

app.Run();