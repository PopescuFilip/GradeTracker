using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces;

/// <summary>
/// Interface for managing student grades.
/// </summary>
public interface IGradeService : IEntityService<GradeEntity>
{
    /// <summary>
    /// Retrieves the list of grades for a specific subject and student.
    /// </summary>
    /// <param name="subjectId">The subject ID.</param>
    /// <param name="studentId">The student ID.</param>
    /// <returns>A list of <see cref="GradeEntity"/> entities.</returns>
    Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId);

    /// <summary>
    /// Retrieves the list of grades for a specific student.
    /// </summary>
    /// <param name="studentId">The student ID.</param>
    /// <returns>A list of <see cref="GradeEntity"/> entities.</returns>
    Task<List<GradeEntity>> GetGradesForStudent(int studentId);

    /// <summary>
    /// Retrieves the list of grades for a specific assignment and student.
    /// </summary>
    /// <param name="studentId">The student ID.</param>
    /// <param name="assignmentId">The assignment ID.</param>
    /// <returns>A list of <see cref="GradeEntity"/> entities.</returns>
    Task<List<GradeEntity>> GetGradesForAssignmentAndStudent(int studentId, int assignmentId);


    /// <summary>
    /// Retrieves grades history for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <returns>A list of corresponding grades.</returns>
    Task<List<GradeEntity>> GetGradesHistoryForTeacher(int teacherId);

    /// <summary>
    /// Checks whether a grade already exists for the given student and assignment.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student.</param>
    /// <param name="assignmentId">The unique identifier of the assignment.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The result is <c>true</c> if a grade exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsForStudentAndAssignment(int studentId, int assignmentId);
}