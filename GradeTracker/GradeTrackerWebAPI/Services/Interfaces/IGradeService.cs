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

    Task<bool> ExistsForStudentAndAssignment(int studentId, int assignmentId);
}