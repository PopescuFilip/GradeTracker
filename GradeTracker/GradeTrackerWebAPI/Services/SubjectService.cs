using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

public class SubjectService(GradeTrackerContext context) : EntityService<SubjectEntity>(context), IEntityService<SubjectEntity>
{
    public async override Task<List<SubjectEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<SubjectEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    public override async Task<SubjectEntity?> Get(int id)
        => await _context.Set<SubjectEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();
}