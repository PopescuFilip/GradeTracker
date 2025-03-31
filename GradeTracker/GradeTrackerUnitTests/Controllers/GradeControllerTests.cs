using FluentAssertions;
using GradeTrackerWebAPI.Controllers;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerUnitTests.Controllers;

[TestClass]
public class GradeControllerTests
{
    private IGradeService _gradeService;
    private GradeController _gradeController;

    [TestInitialize]
    public void Init()
    {
        _gradeService = Substitute.For<IGradeService>();
        _gradeController = new GradeController(_gradeService);
    }

    [TestMethod]
    public async Task CreateGrade_ShouldCallCreateWithCorrectModel_WhenAccessed()
    {
        var createGradeRequest = new CreateGradeRequest(5, 1, 2);
        _gradeService.Create(Arg.Any<GradeEntity>()).Returns(true);

        var result = await _gradeController.Create(createGradeRequest);

        result.Should().BeOfType<OkResult>();
        await _gradeService.Received(1)
            .Create(Arg.Is<GradeEntity>(g =>
            g.Grade == createGradeRequest.Grade &&
            g.StudentId == createGradeRequest.StudentId &&
            g.AssignmentId == createGradeRequest.AssignmentId));
    }

    [TestMethod]
    public async Task GetGradesForSubjectAndStudent_ShouldCallGetGradesForSubjectAndStudentCorrectArgs_WhenAccessed()
    {
        var subjectId = 1234;
        var studentId = 45678;
        var grades = GetGrades();
        _gradeService.GetGradesForSubjectAndStudent(subjectId, studentId).Returns(grades);

        var result = await _gradeController.GetGradesForSubjectAndStudent(subjectId, studentId);

        result.Result.Should().BeOfType<OkObjectResult>();
        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();
        okObjectResult.Value.Should().BeEquivalentTo(grades);
    }

    [TestMethod]
    public async Task GetAllGradesForStudent_ShouldCallGetGradesForStudentCorrectArgs_WhenAccessed()
    {
        var studentId = 3;
        _gradeService.GetGradesForStudent(studentId).Returns([]);

        var result = await _gradeController.GetAllGradesForStudent(studentId);

        await _gradeService.Received(1).GetGradesForStudent(studentId);
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [TestMethod]
    public async Task GetGradesForAssignmentAndStudent_ShouldCallGetGradesForAssignmentAndStudentCorrectArgs_WhenAccessed()
    {
        var assignmentId = 2;
        var studentId = 3;
        var grades = GetGrades();
        _gradeService.GetGradesForAssignmentAndStudent(assignmentId, studentId).Returns(grades);

        var result = await _gradeController.GetGradesForAssignmentAndStudent(assignmentId, studentId);

        await _gradeService.Received(1).GetGradesForAssignmentAndStudent(assignmentId, studentId);
        result.Result.Should().BeOfType<OkObjectResult>();
        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult.Should().NotBeNull();
        okObjectResult.Value.Should().BeEquivalentTo(grades);
    }

    private static List<GradeEntity> GetGrades()
        =>
        [
            new GradeEntity { Id = 1, Grade = 8, DateCreated = DateTime.UtcNow, StudentId = 101, AssignmentId = 201 },
            new GradeEntity { Id = 2, Grade = 6, DateCreated = DateTime.UtcNow, StudentId = 102, AssignmentId = 202 },
            new GradeEntity { Id = 3, Grade = 9, DateCreated = DateTime.UtcNow, StudentId = 103, AssignmentId = 203 },
            new GradeEntity { Id = 4, Grade = 5, DateCreated = DateTime.UtcNow, StudentId = 104, AssignmentId = 204 },
            new GradeEntity { Id = 5, Grade = 7, DateCreated = DateTime.UtcNow, StudentId = 105, AssignmentId = 205 }
        ];

}