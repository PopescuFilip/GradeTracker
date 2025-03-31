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

        var user = await UserService.Login(username, password);

        if (user == null)
        {
            InvalidUsernameOrPassword = true;
            return;
        }

        var userType = await UserService.GetUserType(user.Id);

        if (userType == UserType.None)
        {
            InvalidUsernameOrPassword = true;
            return;
        }

        var role = userType == UserType.Student ? Roles.Student : Roles.Teacher;

        await SignIn(user.Id, role);

        Console.WriteLine("succesful login");
    }

    private async Task SignIn(int id, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, id.ToString()),
            new(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(principal);
        NavigationManager.NavigateTo("/");
    }
}