using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for managing users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user based on their username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="UserEntity"/> object if authentication is successful, otherwise <c>null</c>.</returns>
        Task<UserEntity?> Login(string username, string password);

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="username">The account's username.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns><c>true</c> if the reset was successful, otherwise <c>false</c>.</returns>
        Task<bool> ResetPassword(string username, string newPassword);

        /// <summary>
        /// Retrieves the user type based on their ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user type as a <see cref="UserType"/>.</returns>
        Task<UserType> GetUserType(int id);
    }

}
