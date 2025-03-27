using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

public class StudentService(GradeTrackerContext context) : EntityService<StudentEntity>(context), IEntityService<StudentEntity>
{
    public async override Task<List<StudentEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<StudentEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    public override async Task<StudentEntity?> Get(int id)
        => await _context.Set<StudentEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();
}