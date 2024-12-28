using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace FMSEvalueringUI.Authentication;

public abstract class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthManager _authManager;

    protected SimpleAuthenticationStateProvider(IAuthManager authManager)
    {
        _authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = await _authManager.GetClaimsAsync();
        return new AuthenticationState(principal);
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }
}