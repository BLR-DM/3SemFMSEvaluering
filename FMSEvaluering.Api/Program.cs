using FMSEvaluering.Application;
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

app.MapGet("/post/{id:int}",
    async (int id, IPostQuery postQuery) =>
        await postQuery.GetPost(id));

app.MapPost("/post",
    (CreatePostDto post, IPostCommand command) => 
        command.CreatePost(post));

app.MapDelete("/post", 
    async ([FromBody] DeletePostDto post, IPostCommand command) => 
        await command.DeletePost(post));
    //.RequireAuthorization("isAdmin");


//VOTE
//VOTE
//VOTE
//app.MapPost("/post/vote",
//    (CreateVoteDto dto, IPostCommand command) =>
//        command.CreateVote(dto));
//app.MapPut("/post/vote",
//    (UpdateVoteDto dto, IPostCommand command) =>
//        command.UpdateVote(dto));
//app.MapDelete("/post/vote",
//    (DeleteVoteDto dto, IPostCommand command) =>
//        command.DeleteVote(dto));

app.Run();