using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands.CommandDto.PostDto;
using FMSEvaluering.Application.Commands.CommandDto.VoteDto;
using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Infrastructure;

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
    (CreatePostDto dto, IPostCommand command) =>
        command.CreatePost(dto));

//VOTE
//VOTE
//VOTE
app.MapPost("/post/vote",
    (CreateVoteDto dto, IPostCommand command) =>
        command.CreateVote(dto));
app.MapPut("/post/vote",
    (UpdateVoteDto dto, IPostCommand command) =>
        command.UpdateVote(dto));
app.MapDelete("/post/vote",
    (DeleteVoteDto dto, IPostCommand command) =>
        command.DeleteVote(dto));

app.Run();