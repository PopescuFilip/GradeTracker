using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces;

public interface IGradeService : IEntityService<GradeEntity>
{
    Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId);
}
