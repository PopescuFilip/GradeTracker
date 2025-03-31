using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Helpers;

/// <summary>
/// Provides extension methods for including related entities in queries.
/// </summary>
public static class QueryExtensions
{
    /// <summary>
    /// Includes all related entities for <see cref="SubjectEntity"/>.
    /// </summary>
    /// <param name="query">The queryable object.</param>
    /// <returns>The query including related entities.</returns>
    public static IQueryable<SubjectEntity> IncludeAll(this IQueryable<SubjectEntity> query)
        => query
            .Include(c => c.Students)
            .Include(c => c.Assignments);

    /// <summary>
    /// Includes all related entities for <see cref="StudentEntity"/>.
    /// </summary>
    /// <param name="query">The queryable object.</param>
    /// <returns>The query including related entities.</returns>
    public static IQueryable<StudentEntity> IncludeAll(this IQueryable<StudentEntity> query)
        => query
            .Include(c => c.Subjects)
            .Include(c => c.Grades);

    /// <summary>
    /// Includes all related entities for <see cref="GradeEntity"/>.
    /// </summary>
    /// <param name="query">The queryable object.</param>
    /// <returns>The query including related entities.</returns>
    public static IQueryable<GradeEntity> IncludeAll(this IQueryable<GradeEntity> query)
        => query
            .Include(c => c.Assignment)
            .Include(c => c.Assignment.Subject)
            .Include(c => c.Student);

    /// <summary>
    /// Includes all related entities for <see cref="AssignmentEntity"/>.
    /// </summary>
    /// <param name="query">The queryable object.</param>
    /// <returns>The query including related entities.</returns>
    public static IQueryable<AssignmentEntity> IncludeAll(this IQueryable<AssignmentEntity> query)
        => query
            .Include(c => c.Grades)
            .Include(c => c.Subject);
}