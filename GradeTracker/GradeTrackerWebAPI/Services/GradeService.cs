using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services;

public class GradeService(GradeTrackerContext context) : EntityService<GradeEntity>(context), IGradeService
{
    public async override Task<List<GradeEntity>> GetAll(bool includeAllProperties = false)
    {
        if (!includeAllProperties)
            return await base.GetAll(includeAllProperties);

        return await _context.Set<GradeEntity>()
            .IncludeAll()
            .ToListAsync();
    }

    public override async Task<GradeEntity?> Get(int id)
        => await _context.Set<GradeEntity>()
        .Where(x => x.Id == id)
        .IncludeAll()
        .FirstOrDefaultAsync();

    public async Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
        => await _context.Set<GradeEntity>()
        .Where(g => g.StudentId == studentId)
        .IncludeAll()
        .Where(g => g.Assignment.SubjectId == subjectId)
        .ToListAsync();

}