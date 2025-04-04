using FluentAssertions;
using GradeTracker.Helpers;
using GradeTracker.Models;
using GradeTracker.Services.Interfaces;

namespace GradeTrackerUnitTests.Helpers;

[TestClass]
public class GradeHelperTests
{
    private IGradeService gradeService;
    private IStudentService studentService;
    private GradeHelper gradeHelper;

    [TestInitialize]
    public void Init()
    {
        gradeService = Substitute.For<IGradeService>();
        studentService = Substitute.For<IStudentService>();
        gradeHelper = new(gradeService, studentService);
    }

    [TestMethod]
    public async Task GetGradesForSubject_ShouldCallTheProperFunctions_WhenCalled()
    {
        var subjectId = 1234;
        var students = new List<User>
            {
                new() { Id = 1, Username = "student1", FirstName = "John", LastName = "Doe" },
                new() { Id = 2, Username = "student2", FirstName = "J", LastName = "Smisadth" },
                new() { Id = 3, Username = "student5", FirstName = "agfdne", LastName = "Smithfd" },
                new() { Id = 4, Username = "student67", FirstName = "Jdsaane", LastName = "asaSmith" }
            };
        var studentIds = students.Select(x => x.Id).ToList();
        var grades = GetGrades();

        studentService.GetStudentsForSubject(subjectId).Returns(students);
        gradeService.GetGradesForSubjectAndStudent(subjectId, Arg.Any<int>()).Returns(grades);

        var resultGrades = await gradeHelper.GetGradesForSubject(subjectId);

        await studentService.Received(1).GetStudentsForSubject(subjectId);
        foreach (var studentId in studentIds)
        {
            await gradeService.Received(1).GetGradesForSubjectAndStudent(subjectId, studentId);
        }
        resultGrades.Should().HaveCount(grades.Count * students.Count);
    }

    public List<GradeEntity> GetGrades()
    {
        var gradeEntities = new List<GradeEntity>
        {
            new()
            {
                Id = 1,
                Grade = 95,
                DateCreated = new DateTime(2023, 5, 12),
                StudentId = 1,
                AssignmentId = 101
            },
            new()
            {
                Id = 2,
                Grade = 88,
                DateCreated = new DateTime(2023, 6, 15),
                StudentId = 2,
                AssignmentId = 102
            },
            new()
            {
                Id = 3,
                Grade = 75,
                DateCreated = new DateTime(2023, 7, 10),
                StudentId = 3,
                AssignmentId = 103
            },
            new()
            {
                Id = 4,
                Grade = 90,
                DateCreated = new DateTime(2023, 8, 1),
                StudentId = 4,
                AssignmentId = 104
            },
            new()
            {
                Id = 5,
                Grade = 82,
                DateCreated = new DateTime(2023, 9, 20),
                StudentId = 5,
                AssignmentId = 105
            }
        };

        return gradeEntities;
    }
}