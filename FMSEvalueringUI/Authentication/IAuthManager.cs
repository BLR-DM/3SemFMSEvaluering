using System.Security.Claims;

namespace FMSEvalueringUI.Authentication;

public interface IAuthManager
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetClaimsAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}