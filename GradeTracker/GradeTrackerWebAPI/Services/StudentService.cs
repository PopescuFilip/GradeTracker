using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

/// <summary>
/// Service for managing students.
/// </summary>
public class StudentService(GradeTrackerContext context) : EntityService<StudentEntity>(context), IEntityService<StudentEntity>
{
    /// <summary>
    /// Retrieves all students.
    /// </summary>
    /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
    /// <returns>A list of students.</returns>
    public async override Task<List<StudentEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<StudentEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a student by ID.
    /// </summary>
    /// <param name="id">The student ID.</param>
    /// <returns>The found student or <c>null</c> if not found.</returns>
    public override async Task<StudentEntity?> Get(int id)
        => await _context.Set<StudentEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();
}