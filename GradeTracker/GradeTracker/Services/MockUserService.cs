using GradeTracker.Enums;
using GradeTracker.Services.Interfaces;

namespace GradeTracker.Services;

public class MockUserService : IUserService
{
    private string studentPassword = "pass";
    private string teacherPassword = "pass";

    public async Task<UserType> Login(string username, string password)
    {
        if (username == "student" && password == studentPassword)
            return UserType.Student;
        if (username == "teacher" && password == teacherPassword)
            return UserType.Teacher;

        return UserType.None;
    }

    public async Task<bool> ResetPassword(string username, string newPassword)
    {
        if (username == "student")
        {
            studentPassword = newPassword;
            return true;
        }

        if (username == "teacher")
        {
            teacherPassword = newPassword;
            return true;
        }

        return false;
    }
}