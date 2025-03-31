using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    /// <summary>
    /// Service for managing assignments.
    /// </summary>
    public class AssignmentService(GradeTrackerContext context) : EntityService<AssignmentEntity>(context), IEntityService<AssignmentEntity>
    {
        /// <summary>
        /// Retrieves all assignments.
        /// </summary>
        /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
        /// <returns>A list of assignments.</returns>
        public async override Task<List<AssignmentEntity>> GetAll(bool includeAllProperties = false)
        {
            if (!includeAllProperties)
                return await base.GetAll(includeAllProperties);

            return await _context.Set<AssignmentEntity>()
                .IncludeAll()
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves an assignment by ID.
        /// </summary>
        /// <param name="id">The assignment ID.</param>
        /// <returns>The found assignment or <c>null</c> if not found.</returns>
        public override async Task<AssignmentEntity?> Get(int id)
            => await _context.Set<AssignmentEntity>()
            .Where(x => x.Id == id)
            .IncludeAll()
            .FirstOrDefaultAsync();
    }
}
