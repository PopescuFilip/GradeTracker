using GradeTracker.Enums;
using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces;

public interface IUserService
{
    Task<User?> Login(string username, string password);

    Task<bool> ResetPassword(string username, string newPassword);

    Task<UserType> GetUserType(int id);
    Task<User?> GetUserById(int id);
}