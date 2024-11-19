using Gateway.API.ExternalServices;
using Gateway.API.Interfaces;
using Gateway.API.ModelDto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddHttpClient();
builder.Services.AddScoped<IFmsProxy, FmsProxy>();

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.MapGet("/login", async (LoginDto loginDto, HttpClient httpClient) =>
//{
//    var respone = await httpClient.PostAsJsonAsync("http://fmsdataserver.api:8080/fms/login", loginDto);

//    return Results.Ok(respone);
//});

app.MapPost("/login", async (LoginDto loginDto, IFmsProxy fmsproxy) =>
{
    var response = await fmsproxy.CheckCredentials(loginDto.Email, loginDto.Password);
    return Results.Ok(response);
});


app.MapReverseProxy();

app.Run();