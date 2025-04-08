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

    /// <summary>
    /// Asynchronously creates a new grade entry if one does not already exist for the specified student and assignment.
    /// </summary>
    /// <param name="model">The <see cref="GradeEntity"/> to be created.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is 
    /// <c>true</c> if the grade was successfully created; otherwise, <c>false</c> 
    /// if a grade for the given student and assignment already exists.
    /// </returns>
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
    /// Retrieves grades history for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher ID.</param>
    /// <returns>A list of corresponding grades.</returns>
    public async Task<List<GradeEntity>> GetGradesHistoryForTeacher(int teacherId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.Assignment.Subject.Teacher.Id == teacherId)
        .IncludeAll()
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

    /// <summary>
    /// Checks asynchronously whether a grade already exists for the given student and assignment.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student.</param>
    /// <param name="assignmentId">The unique identifier of the assignment.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is 
    /// <c>true</c> if a grade exists for the specified student and assignment; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> ExistsForStudentAndAssignment(int studentId, int assignmentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId && g.AssignmentId == assignmentId)
        .AnyAsync();
}