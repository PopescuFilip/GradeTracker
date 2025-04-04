using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

/// <summary>
/// Service for managing grades.
/// </summary>
public class GradeService(GradeTrackerContext context) : EntityService<GradeEntity>(context), IGradeService
{
    /// <summary>
    /// Retrieves all grades.
    /// </summary>
    /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
    /// <returns>A list of grades.</returns>
    public async override Task<List<GradeEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<GradeEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a grade by ID.
    /// </summary>
    /// <param name="id">The grade ID.</param>
    /// <returns>The found grade or <c>null</c> if not found.</returns>
    public override async Task<GradeEntity?> Get(int id)
        => await _context.Set<GradeEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();

    public override async Task<bool> Create(GradeEntity model)
    {
        var gradeExists = await ExistsForStudentAndAssignment(model.StudentId, model.AssignmentId);

        if (gradeExists)
            return false;

        return await base.Create(model);
    }

    /// <summary>
    /// Retrieves grades for a specific subject and student.
    /// </summary>
    /// <param name="subjectId">The subject ID.</param>
    /// <param name="studentId">The student ID.</param>
    /// <returns>A list of corresponding grades.</returns>
    public async Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId)
        .IncludeAll()
        .Where(g => g.Assignment.SubjectId == subjectId)
        .ToListAsync();

    /// <summary>
    /// Retrieves all grades for a student.
    /// </summary>
    /// <param name="studentId">The student ID.</param>
    /// <returns>A list of grades for the student.</returns>
    public async Task<List<GradeEntity>> GetGradesForStudent(int studentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId)
        .IncludeAll()
        .ToListAsync();

    /// <summary>
    /// Retrieves grades for a specific assignment and student.
    /// </summary>
    /// <param name="studentId">The student ID.</param>
    /// <param name="assignmentId">The assignment ID.</param>
    /// <returns>A list of corresponding grades.</returns>
    public async Task<List<GradeEntity>> GetGradesForAssignmentAndStudent(int studentId, int assignmentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId)
        .IncludeAll()
        .Where(g => g.AssignmentId == assignmentId)
        .ToListAsync();

    public async Task<bool> ExistsForStudentAndAssignment(int studentId, int assignmentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId && g.AssignmentId == assignmentId)
        .AnyAsync();
}