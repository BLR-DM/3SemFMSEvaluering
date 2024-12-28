using FMSEvalueringUI.Authentication;
using FMSEvalueringUI.Components;
using FMSEvalueringUI.ExternalServices;
using FMSEvalueringUI.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<IDataServerProxy, DataServerProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["FmsDataProxy:BaseAddress"]);
});
builder.Services.AddHttpClient<IEvalueringProxy, EvalueringProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["FmsEvalueringProxy:BaseAddress"]);
});

builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
