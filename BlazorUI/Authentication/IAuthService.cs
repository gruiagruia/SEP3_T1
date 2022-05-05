using System.Security.Claims;

namespace BlazorUI.Authentication;

public interface IAuthService
{
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}