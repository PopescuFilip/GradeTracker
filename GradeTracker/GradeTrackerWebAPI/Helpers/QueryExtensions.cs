using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Helpers;

public static class QueryExtensions
{
    public static IQueryable<SubjectEntity> IncludeAll(this IQueryable<SubjectEntity> query)
        => query
            .Include(c => c.Students)
            .Include(c => c.Assignments);
}