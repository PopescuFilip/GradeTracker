using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

/// <summary>
/// Service for managing subjects.
/// </summary>
public class SubjectService(GradeTrackerContext context) : EntityService<SubjectEntity>(context), ISubjectService
{
    /// <summary>
    /// Retrieves all subjects.
    /// </summary>
    /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
    /// <returns>A list of subjects.</returns>
    public async override Task<List<SubjectEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<SubjectEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a subject by ID.
    /// </summary>
    /// <param name="id">The subject ID.</param>
    /// <returns>The found subject or <c>null</c> if not found.</returns>
    public override async Task<SubjectEntity?> Get(int id)
        => await _context.Set<SubjectEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();

    /// <summary>
    /// Asynchronously retrieves the subject assigned to a specific teacher.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the <see cref="SubjectEntity"/> associated with the specified teacher.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if there is not exactly one subject for the given teacher (e.g., if none or multiple are found).
    /// </exception>
    public async Task<SubjectEntity> GetSubjectForTeacher(int teacherId)
        => await _context.Set<SubjectEntity>()
        .Include(s => s.Teacher)
        .Where(s => s.Teacher.Id == teacherId)
        .SingleAsync();
}