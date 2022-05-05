using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService authService;


    public AuthStateProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)));
    }
}