using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces;

/// <summary>
/// Provides subject-specific operations, extending the base entity service for <see cref="SubjectEntity"/>.
/// </summary>
public interface ISubjectService : IEntityService<SubjectEntity>
{
    /// <summary>
    /// Retrieves the subject assigned to the specified teacher.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The result contains 
    /// the <see cref="SubjectEntity"/> associated with the given teacher.
    /// </returns>
    Task<SubjectEntity> GetSubjectForTeacher(int teacherId);
}