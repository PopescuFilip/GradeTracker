using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Helpers;

public static class QueryExtensions
{
    public static IQueryable<ClassEntity> IncludeAll(this IQueryable<ClassEntity> query)
        => query
            .Include(c => c.Students)
            .Include(c => c.Subjects);
}
