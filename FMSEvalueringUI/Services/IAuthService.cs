using System.Security.Claims;
using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.Services;

public interface IAuthService
{
    public Task LoginAsync(LoginDto loginDto);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetClaimsAsync();
    public Task<string> GetJwtTokenAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }

    public string? Jwt { get; }
}