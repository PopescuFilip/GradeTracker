using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

public class ClassService(GradeTrackerContext context) : EntityService<ClassEntity>(context), IEntityService<ClassEntity>
{
    public async override Task<List<ClassEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<ClassEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    public override async Task<ClassEntity?> Get(int id)
        => await _context.Set<ClassEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstAsync();
}