using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    /// <summary>
    /// Service for managing users.
    /// </summary>
    public class UserService(GradeTrackerContext context) : IUserService
    {
        /// <summary>
        /// Database context instance.
        /// </summary>
        private readonly GradeTrackerContext _context = context;

        /// <summary>
        /// Resets the password for a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="newPassword">The new password to be set.</param>
        /// <returns><c>true</c> if the password was successfully reset; otherwise, <c>false</c>.</returns>
        public async Task<bool> ResetPassword(string username, string newPassword)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (foundUser == null)
                return false;

            foundUser.Password = newPassword;
            _context.Users.Update(foundUser);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Authenticates a user based on username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user entity or <c>null</c> if authentication fails.</returns>
        public async Task<UserEntity?> Login(string username, string password) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        /// <summary>
        /// Determines the type of user based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user type: Student, Teacher, or None if not found.</returns>
        public async Task<UserType> GetUserType(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
                return UserType.Student;

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
                return UserType.Teacher;

            return UserType.None;
        }
    }
}