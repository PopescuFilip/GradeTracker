using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<List<Assignment>?> GetAssignments();
    }
}
