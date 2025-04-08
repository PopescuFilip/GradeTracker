using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing grade-related actions, such as creating and retrieving grades.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GradeController(IGradeService gradeService) : BaseEntityController<GradeEntity>(gradeService)
{
    private readonly IGradeService _gradeService = gradeService;

    /// <summary>
    /// Creates a new grade for a student in a specific assignment.
    /// </summary>
    /// <param name="createGradeRequest">The request data containing the grade, student ID, and assignment ID.</param>
    /// <returns>A status indicating whether the grade was successfully created.</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateGradeRequest createGradeRequest)
    {
        var newGrade = new GradeEntity()
        {
            Grade = createGradeRequest.Grade,
            StudentId = createGradeRequest.StudentId,
            AssignmentId = createGradeRequest.AssignmentId,
        };

        var success = await _entityService.Create(newGrade);

        if (!success)
            return BadRequest();

        return Ok();
    }

    /// <summary>
    /// Retrieves grades for a specific student in a specific subject.
    /// </summary>
    /// <param name="subjectId">The ID of the subject.</param>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>A list of grades for the specified student and subject.</returns>
    [HttpGet("get-grades-for-subject-and-student/{subjectId}/{studentId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
    {
        var grades = await _gradeService.GetGradesForSubjectAndStudent(subjectId, studentId);

        if (grades == null)
            return NotFound();

        return Ok(grades);
    }

    /// <summary>
    /// Retrieves all grades for a specific student.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>A list of all grades for the specified student.</returns>
    [HttpGet("get-all-grades/{studentId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetAllGradesForStudent(int studentId)
    {
        var grades = await _gradeService.GetGradesForStudent(studentId);

        if (grades == null || grades.Count == 0)
            return NotFound();

        return Ok(grades);
    }

    /// <summary>
    /// Retrieves grades for a specific student in a specific assignment.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <param name="assignmentId">The ID of the assignment.</param>
    /// <returns>A list of grades for the specified student and assignment.</returns>
    [HttpGet("get-grades-for-assignment/{studentId}/{assignmentId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetGradesForAssignmentAndStudent(int studentId, int assignmentId)
    {
        var grades = await _gradeService.GetGradesForAssignmentAndStudent(studentId, assignmentId);

        if (grades == null || grades.Count == 0)
            return NotFound();

        return Ok(grades);
    }


    /// <summary>
    /// Retrieves the list of grades assigned by the specified teacher.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher.</param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing the list of <see cref="GradeEntity"/> instances.
    /// </returns>
    /// <response code="200">Returns the list of grades assigned by the teacher.</response>
    /// <response code="404">If no grades are found.</response>
    [HttpGet("get-grades-history-for-teacher/{teacherId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetGradesHistoryForTeacher(int teacherId)
    {
        var grades = await _gradeService.GetGradesHistoryForTeacher(teacherId);

        if (grades == null || grades.Count == 0)
            return NotFound();

        return Ok(grades);
    }

    /// <summary>
    /// Updates the grade value for a specific grade entity.
    /// </summary>
    /// <param name="id">The unique identifier of the grade to update.</param>
    /// <param name="newGrade">The new grade value to assign.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> indicating the result of the operation.
    /// </returns>
    /// <response code="200">Grade was successfully updated.</response>
    /// <response code="400">If the update fails.</response>
    /// <response code="404">If the grade was not found.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] int newGrade)
    {
        var foundGrade = await _entityService.Get(id);
        if (foundGrade == null)
            return NotFound();

        foundGrade.Grade = newGrade;
        var success = await _entityService.Update(foundGrade);

        if (!success)
            return BadRequest("Something went wrong while updating grade");

        return Ok();
    }

    /// <summary>
    /// Checks whether a grade exists for a specific student and assignment.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student.</param>
    /// <param name="assignmentId">The unique identifier of the assignment.</param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing a boolean value indicating whether the grade exists.
    /// </returns>
    /// <response code="200">Returns <c>true</c> if the grade exists; otherwise, <c>false</c>.</response>
    [HttpGet("exists-for-student-and-assignment/{studentId}/{assignmentId}")]
    public async Task<ActionResult<List<GradeEntity>>> ExistsForStudentAndAssignment(int studentId, int assignmentId)
    {
        var exists = await _gradeService.ExistsForStudentAndAssignment(studentId, assignmentId);

        return Ok(exists);
    }
}

/// <summary>
/// Represents the request data for creating a new grade.
/// </summary>
public record CreateGradeRequest(int Grade, int StudentId, int AssignmentId);
