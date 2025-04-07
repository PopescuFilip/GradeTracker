using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerUnitTests.Services
{
    [TestClass]
    public class StudentServiceTests
    {
        private DbContextOptions<GradeTrackerContext> _dbContextOptions = null!;

        [TestInitialize]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<GradeTrackerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestCleanup]
        public void Cleanup()
        {
            using var context = new GradeTrackerContext(_dbContextOptions);
            context.Database.EnsureDeleted(); // Delete the in-memory database after each test
        }

        [TestMethod]
        public async Task AddSubjectMethod_ShouldAddSubject()
        {
            // Arrange
            using var context = new GradeTrackerContext(_dbContextOptions);
            var studentService = new StudentService(context);

            // Create a student with an empty list of subjects.
            var student = new StudentEntity
            {
                Id = 1,
                Subjects = new List<SubjectEntity>()
            };

            // Create a subject entity.
            var subject = new SubjectEntity
            {
                Id = 1,
                // Add any additional properties as needed (e.g., Name, Description).
            };

            await context.Students.AddAsync(student);
            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();

            // Act
            bool result = await studentService.AddSubject(1, 1);

            // Assert
            Assert.IsTrue(result, "The subject should have been added successfully.");

            // Verify that the student's subjects list now includes the added subject.
            var updatedStudent = await context.Students
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == 1);
            Assert.IsNotNull(updatedStudent);
            Assert.IsTrue(updatedStudent.Subjects.Any(s => s.Id == 1), "The subject was not found in the student's subjects list.");
        }

        [TestMethod]
        public async Task RemoveSubjectMethod_ShouldRemoveSubject()
        {
            // Arrange
            using var context = new GradeTrackerContext(_dbContextOptions);
            var studentService = new StudentService(context);

            // Create a subject entity.
            var subject = new SubjectEntity
            {
                Id = 1,
                // Add any additional properties as needed.
            };

            // Create a student entity with the subject already added.
            var student = new StudentEntity
            {
                Id = 1,
                Subjects = new List<SubjectEntity> { subject }
            };

            await context.Subjects.AddAsync(subject);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            // Act
            bool result = await studentService.RemoveSubject(1, 1);

            // Assert
            Assert.IsTrue(result, "The subject should have been removed successfully.");

            // Verify that the student's subjects list no longer includes the removed subject.
            var updatedStudent = await context.Students
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == 1);
            Assert.IsNotNull(updatedStudent);
            Assert.IsFalse(updatedStudent.Subjects.Any(s => s.Id == 1), "The subject is still present in the student's subjects list.");
        }
    }
}
