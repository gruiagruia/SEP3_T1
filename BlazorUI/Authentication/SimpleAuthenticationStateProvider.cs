using System.Security.Claims;
using Domain.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Authentication;
public class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
private readonly IAuth authService;

public SimpleAuthenticationStateProvider(IAuth authService)
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
                new AuthenticationState(principal)
            )
        );
    }
}