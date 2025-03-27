using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Helpers;

public static class QueryExtensions
{
    public static IQueryable<SubjectEntity> IncludeAll(this IQueryable<SubjectEntity> query)
        => query
            .Include(c => c.Students)
            .Include(c => c.Assignments);

    public static IQueryable<StudentEntity> IncludeAll(this IQueryable<StudentEntity> query)
        => query
            .Include(c => c.Subjects)
            .Include(c => c.Grades);

    public static IQueryable<GradeEntity> IncludeAll(this IQueryable<GradeEntity> query)
        => query
            .Include(c => c.Assignment)
            .Include(c => c.Assignment.Subject)
            .Include(c => c.Student);
}