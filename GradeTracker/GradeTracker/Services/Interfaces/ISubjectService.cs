using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>?> GetAllSubjects();
        Task<List<Subject>?> GetSubjectsForStudent(int studentId);
    }
}
