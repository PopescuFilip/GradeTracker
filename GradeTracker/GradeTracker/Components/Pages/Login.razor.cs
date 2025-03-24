using GradeTracker.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace GradeTracker.Components.Pages;

public partial class Login
{
    [CascadingParameter]
    public HttpContext HttpContext { get; set; }



    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private bool InvalidUsernameOrPassword { get; set; }

    [SupplyParameterFromForm]
    public LoginViewModel Model { get; set; } = new();

    public async Task Authenticate()
    {
        var username = Model.Username;
        var password = Model.Password;

        await SignIn(username);

        Console.WriteLine("succesful login");
    }

    private async Task SignIn(string username)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "student")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(principal);
        NavigationManager.NavigateTo("/");
    }
}