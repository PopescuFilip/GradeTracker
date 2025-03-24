using GradeTracker.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GradeTracker.Components.Pages
{
    public partial class ResetPassword
    {
        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string Username { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;

        private bool InvalidUsername { get; set; }

        private async Task Reset()
        {
            InvalidUsername = false;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                return;
            var succesfullReset = await UserService.ResetPassword(Username, Password);

            if (!succesfullReset)
            {
                InvalidUsername = true;
                return;
            }

            NavigationManager.NavigateTo("/login");
        }
    }
}