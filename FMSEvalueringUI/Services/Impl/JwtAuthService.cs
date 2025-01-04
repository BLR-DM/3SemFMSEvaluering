using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.JSInterop;

namespace FMSEvalueringUI.Services.Impl;

public class JwtAuthService : IAuthService
{
    private readonly IDataServerProxy _dataServerProxy;
    private readonly IJSRuntime _jsRuntime;

    public JwtAuthService(IDataServerProxy dataServerProxy, IJSRuntime jsRuntime)
    {
        _dataServerProxy = dataServerProxy;
        _jsRuntime = jsRuntime;
    }
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    // this private variable for simple caching
    public string? Jwt { get; private set; } = "";

    async Task IAuthService.LoginAsync(LoginDto loginDto)
    {
        var tokenResponse = await _dataServerProxy.CheckCredentials(loginDto);

        Jwt = tokenResponse.Token;

        await CacheTokenAsync(Jwt);

        var claims = ParseClaimsFromJwt(tokenResponse.Token);

        OnAuthStateChanged.Invoke(claims);
    }

    private async Task CacheTokenAsync(string jwt)
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", jwt);
    }

    async Task IAuthService.LogoutAsync()
    {
        Jwt = null;
        await ClearTokenFromCacheAsync();
        OnAuthStateChanged.Invoke(new ClaimsPrincipal());
    }

    private async Task ClearTokenFromCacheAsync()
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    async Task<string> IAuthService.GetJwtTokenAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");

        return token;
    }

    async Task<ClaimsPrincipal> IAuthService.GetClaimsAsync()
    {
        var currentUser = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        
        if (string.IsNullOrEmpty(currentUser))
            throw new ArgumentException("No user found!");

        return ParseClaimsFromJwt(currentUser);
    }


    private ClaimsPrincipal ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();

        var claimsIdentity = new ClaimsIdentity(claims, "jwtAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }

}