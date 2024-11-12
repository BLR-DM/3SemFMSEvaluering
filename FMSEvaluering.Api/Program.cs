using FMSEvaluering.Application;
using FMSEvaluering.Application.Commands;
using FMSEvaluering.Application.Commands.CommandDto.EvaluationPostDto;
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
    (CreateEvaluationPostDto evaluationPost, IEvaluationPostCommand command) =>
        command.CreateEvaluationPost(evaluationPost));

app.Run();