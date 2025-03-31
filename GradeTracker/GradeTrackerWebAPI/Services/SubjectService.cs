﻿using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

/// <summary>
/// Service for managing subjects.
/// </summary>
public class SubjectService(GradeTrackerContext context) : EntityService<SubjectEntity>(context), IEntityService<SubjectEntity>
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
}