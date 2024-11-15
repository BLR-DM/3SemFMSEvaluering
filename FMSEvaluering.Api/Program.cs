using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;
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

app.MapPost("/evaluationpost",
    (CreatePostDto evaluationPost, IPostCommand command) =>
        command.CreatePost(evaluationPost));

app.Run();