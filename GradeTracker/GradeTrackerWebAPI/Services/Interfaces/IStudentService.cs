using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    /// <summary>
    /// Interface for student-related operations.
    /// </summary>
    public interface IStudentService : IEntityService<StudentEntity>
    {
        /// <summary>
        /// Adds a subject to a student.
        /// </summary>
        /// <param name="userId">The unique identifier of the student.</param>
        /// <param name="subjectId">The unique identifier of the subject to add.</param>
        /// <returns>
        /// <c>true</c> if the subject was successfully added; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> AddSubject(int userId, int subjectId);

        /// <summary>
        /// Removes a subject from a student's list of subjects.
        /// </summary>
        /// <param name="userId">The unique identifier of the student.</param>
        /// <param name="subjectId">The unique identifier of the subject to remove.</param>
        /// <returns>
        /// <c>true</c> if the subject was successfully removed; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> RemoveSubject(int userId, int subjectId);
    }
}
