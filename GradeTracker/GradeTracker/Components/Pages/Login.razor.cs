using GradeTracker.Enums;
using GradeTracker.Services.Interfaces;
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
    public IUserService UserService { get; set; }


    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private bool InvalidUsernameOrPassword { get; set; }

    [SupplyParameterFromForm]
    public LoginViewModel Model { get; set; } = new();

    public async Task Authenticate()
    {
        var username = Model.Username;
        var password = Model.Password;

        var userType = await UserService.Login(username, password);

        if (userType == UserType.None)
        {
            InvalidUsernameOrPassword = true;
            return;
        }

        var role = userType == UserType.Student ? Roles.Student : Roles.Teacher;

        await SignIn(username, role);

        Console.WriteLine("succesful login");
    }

    private async Task SignIn(string username, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(principal);
        NavigationManager.NavigateTo("/");
    }
}