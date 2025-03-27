using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity?> Login(string username, string password);

        Task<bool> ResetPassword(string username, string newPassword);

        Task<UserType> GetUserType(int id);
    }
}
