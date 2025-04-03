using GradeTracker.Models;

namespace GradeTracker.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<List<Assignment>?> GetAssignments();
        Task<bool> CreateAssignment(CreateAssignmentRequest createAssignmentRequest);
    }

    public record CreateAssignmentRequest(string Title, string Description, int SubjectId);
}
