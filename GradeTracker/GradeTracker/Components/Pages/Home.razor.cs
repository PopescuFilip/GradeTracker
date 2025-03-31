using GradeTracker.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace GradeTracker.Components.Pages
{
    public partial class Home
    {
        private User? user;
        private int userId;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity.Name != null)
            {
                userId = int.Parse(authState.User.Identity.Name);
                user = new User();
                user = await UserService.GetUserById(userId);
            }

        }
    }
}