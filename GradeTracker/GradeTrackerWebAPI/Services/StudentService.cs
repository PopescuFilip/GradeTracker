using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Helpers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Services
{
    /// <summary>
    /// Service for managing students.
    /// </summary>
    public class StudentService : EntityService<StudentEntity>, IStudentService
    {
        public StudentService(GradeTrackerContext context) : base(context)
        {
        }

        /// <summary>
        /// Adds a subject to a student.
        /// </summary>
        /// <param name="userId">The unique identifier of the student.</param>
        /// <param name="subjectId">The unique identifier of the subject to add.</param>
        /// <returns>
        /// <c>true</c> if the subject was successfully added; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AddSubject(int userId, int subjectId)
        {
            StudentEntity? student = await Get(userId);
            SubjectEntity? subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == subjectId);

            if (student == null || subject == null)
                return false;

            int initialSubjectsCount = student.Subjects.Count;
            student.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            // Verify that the student exists and that the count of subjects has increased.
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) != null && student.Subjects.Count > initialSubjectsCount;
        }

        /// <summary>
        /// Removes a subject from a student's list of subjects.
        /// </summary>
        /// <param name="userId">The unique identifier of the student.</param>
        /// <param name="subjectId">The unique identifier of the subject to remove.</param>
        /// <returns>
        /// <c>true</c> if the subject was successfully removed; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> RemoveSubject(int userId, int subjectId)
        {
            StudentEntity? student = await Get(userId);
            if (student == null)
            {
                return false;
            }

            // Attempt to find the subject in the student's subjects list.
            SubjectEntity? subject = student.Subjects.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null)
            {
                return false;
            }

            int initialSubjectsCount = student.Subjects.Count;
            student.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            // Confirm that the subject count has decreased.
            return student.Subjects.Count < initialSubjectsCount;
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
        /// <returns>A list of students.</returns>
        public async override Task<List<StudentEntity>> GetAll(bool includeAllProperties = false)
        {
            if (!includeAllProperties)
                return await base.GetAll(includeAllProperties);

            return await _context.Set<StudentEntity>()
                .IncludeAll()
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns>The found student or <c>null</c> if not found.</returns>
        public override async Task<StudentEntity?> Get(int id)
            => await _context.Set<StudentEntity>()
                .Where(x => x.Id == id)
                .IncludeAll()
                .FirstOrDefaultAsync();
    }
}
