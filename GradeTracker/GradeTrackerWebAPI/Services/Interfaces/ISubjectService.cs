using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces;

public interface ISubjectService : IEntityService<SubjectEntity>
{
    Task<SubjectEntity> GetSubjectForTeacher(int teacherId);
}