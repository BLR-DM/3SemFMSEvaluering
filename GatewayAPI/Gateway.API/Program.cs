using Gateway.API.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/login", async (LoginDto loginDto, HttpClient httpClient) =>
{
    var respone = await httpClient.PostAsJsonAsync("http://fmsdataserver.api:8080/fms/login", loginDto);

    return Results.Ok(respone);
});

app.MapReverseProxy();

app.Run();
