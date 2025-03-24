using GradeTracker.Enums;

namespace GradeTracker.Services.Interfaces;

public interface IUserService
{
    Task<UserType> Login(string username, string password);

    Task<bool> ResetPassword(string username, string newPassword);
}