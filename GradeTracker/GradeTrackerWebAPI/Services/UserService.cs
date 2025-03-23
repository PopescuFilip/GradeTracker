using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly GradeTrackerContext _context;

        public UserService(GradeTrackerContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangePassword(int id, string oldPassword, string newPassword)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (foundUser == null) return false;

            if (foundUser.Password != oldPassword) return false;

            foundUser.Password = newPassword;
            _context.Users.Update(foundUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserEntity?> Login(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
