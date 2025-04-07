using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces;

public interface IStudentService
{
    Task<List<User>> GetStudentsForSubject(int subjectId);

    public Task<bool> AddSubject(int userId, int subjectId);

    Task<bool> RemoveSubject(int userId, int subjectId);
}
