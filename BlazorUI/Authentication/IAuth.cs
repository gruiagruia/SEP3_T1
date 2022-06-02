using System.Security.Claims;
using Domain.Model;

namespace BlazorUI.Authentication;

public interface IAuth
{
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }

    Task<User?> GetUserFromCacheAsync();
}