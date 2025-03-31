using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    public class AssignmentService(GradeTrackerContext context) : EntityService<AssignmentEntity>(context), IEntityService<AssignmentEntity>
    {
        public async override Task<List<AssignmentEntity>> GetAll(bool includeAllProperties = false)
        {
            if (!includeAllProperties)
                return await base.GetAll(includeAllProperties);

            return await _context.Set<AssignmentEntity>()
                .IncludeAll()
                .ToListAsync();
        }

        public override async Task<AssignmentEntity?> Get(int id)
            => await _context.Set<AssignmentEntity>()
            .Where(x => x.Id == id)
            .IncludeAll()
            .FirstOrDefaultAsync();

    }
}
