using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing student-related actions, including retrieving students for a subject,
/// and adding or removing subjects from a student.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StudentController(
    IStudentService studentService,
    IEntityService<StudentEntity> entityService,
    IEntityService<SubjectEntity> subjectService) : BaseEntityController<StudentEntity>(entityService)
{
    private readonly IStudentService _studentService = studentService;
    private readonly IEntityService<SubjectEntity> _subjectService = subjectService;

    /// <summary>
    /// Retrieves the students enrolled in a specific subject.
    /// </summary>
    /// <param name="subjectId">The ID of the subject.</param>
    /// <returns>A list of students enrolled in the specified subject.</returns>
    [HttpGet("get-students-for-subject/{subjectId}")]
    public async Task<ActionResult<List<StudentEntity>>> GetStudentsForSubject(int subjectId)
    {
        var foundSubject = await _subjectService.Get(subjectId);

        if (foundSubject == null)
            return NotFound();

        return Ok(foundSubject.Students);
    }

    /// <summary>
    /// Adds a subject to a student's list of subjects.
    /// </summary>
    /// <param name="userId">The unique identifier of the student.</param>
    /// <param name="subjectId">The unique identifier of the subject to add.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> with status code 200 (OK) if successful, otherwise 400 (Bad Request).
    /// </returns>
    [HttpPost("add-subject/{userId}/{subjectId}")]
    public async Task<ActionResult> AddSubject(int userId, int subjectId)
    {
        bool result = await _studentService.AddSubject(userId, subjectId);
        if (!result)
            return BadRequest("Failed to add subject to student.");
        return Ok();
    }

    /// <summary>
    /// Removes a subject from a student's list of subjects.
    /// </summary>
    /// <param name="userId">The unique identifier of the student.</param>
    /// <param name="subjectId">The unique identifier of the subject to remove.</param>
    /// <returns>
    /// An <see cref="ActionResult"/> with status code 200 (OK) if successful, otherwise 400 (Bad Request).
    /// </returns>
    [HttpPost("remove-subject/{userId}/{subjectId}")]
    public async Task<ActionResult> RemoveSubject(int userId, int subjectId)
    {
        bool result = await _studentService.RemoveSubject(userId, subjectId);
        if (!result)
            return BadRequest("Failed to remove subject from student.");
        return Ok();
    }
}
