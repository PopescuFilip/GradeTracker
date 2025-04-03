using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces;

public interface IStudentService
{
    Task<List<User>> GetStudentsForSubject(int subbjectId);
}
