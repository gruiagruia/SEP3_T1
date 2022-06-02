using System.Security.Claims;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Model;
using Microsoft.JSInterop;

namespace BlazorUI.Authentication;
public class AuthImpl : IAuth
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!; // assigning to null! to suppress null warning.
    private readonly IUser userService;
    private readonly IJSRuntime jsRuntime;

    public AuthImpl(IUser userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }

    public async Task LoginAsync(string email, string password)
    {
        User? user = await userService.GetUserByEmailAsync(email);
        Console.WriteLine(user.FirstName + " got from server");
        
        ValidateLoginCredentials(password, user);

        await CacheUserAsync(user!); // Cache the user object in the browser 

        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        OnAuthStateChanged?.Invoke(principal);
       
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync();
        
        ClaimsPrincipal principal = CreateClaimsPrincipal(null);
        
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task<ClaimsPrincipal> GetAuthAsync() 
    {
        User? user =  await GetUserFromCacheAsync(); 

        ClaimsPrincipal principal = CreateClaimsPrincipal(user);

        return principal;
    }

    public async Task<User?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        User user = JsonSerializer.Deserialize<User>(userAsJson)!;
        return user;
    }

    private static void ValidateLoginCredentials(string password, User? user)
    {
        if (user == null)
        {
            throw new Exception("Email not found");
        }

        if (!string.Equals(password,user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }
        return new ClaimsPrincipal();
    }

    private async Task CacheUserAsync(User user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    private static ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim("Last Name", user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Password", user.Password),
            new Claim("Role", user.Role)
        };
        return new ClaimsIdentity(claims, "apiauth_type");
    }
}