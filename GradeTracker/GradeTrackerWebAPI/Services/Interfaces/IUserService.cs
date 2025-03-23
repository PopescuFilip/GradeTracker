using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity?> Login(string username, string password);

        Task<bool> ChangePassword(int id, string oldPassword, string newPassword);
    }
}
