using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Enums;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    public class UserService(GradeTrackerContext context) : IUserService
    {
        private readonly GradeTrackerContext _context = context;

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

        public async Task<UserEntity?> Login(string username, string password) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

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