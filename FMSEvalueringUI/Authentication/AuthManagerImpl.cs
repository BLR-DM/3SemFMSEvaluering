using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FMSEvalueringUI.Authentication;

public class AuthManagerImpl : IAuthManager
{
    private readonly IDataServerProxy _dataServerProxy;
    private readonly ProtectedSessionStorage _protectedSessionStorage;

    public AuthManagerImpl(IDataServerProxy dataServerProxy, ProtectedSessionStorage protectedSessionStorage)
    {
        _dataServerProxy = dataServerProxy;
        _protectedSessionStorage = protectedSessionStorage;
    }

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    async Task<ClaimsPrincipal> IAuthManager.GetClaimsAsync()
    {
        var result = await _protectedSessionStorage.GetAsync<string>("authToken");

        if (!result.Success)
            throw new ArgumentException("No user found!");

        return ParseClaimsFromJwt(result.Value!);
    }
    
    async Task IAuthManager.LoginAsync(string username, string password)
    {
        var user = new LoginDto{ Email = username, Password = password};
        var tokenResponse = await _dataServerProxy.CheckCredentials(user);
        await _protectedSessionStorage.SetAsync("authToken", tokenResponse.Token);

        var claims = ParseClaimsFromJwt(tokenResponse.Token);

        OnAuthStateChanged.Invoke(claims);
    }

    async Task IAuthManager.LogoutAsync()
    {
        await _protectedSessionStorage.DeleteAsync("authToken");
        
        OnAuthStateChanged.Invoke(new ClaimsPrincipal());
    }

    private static ClaimsPrincipal ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();

        var claimsIdentity = new ClaimsIdentity(claims, "jwtAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }
}