using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces;

public interface IGradeService : IEntityService<GradeEntity>
{
    Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId);
    Task<List<GradeEntity>> GetGradesForStudent(int studentId);
    Task<List<GradeEntity>> GetGradesForAssignmentAndStudent(int studentId, int assignmentId);

}
