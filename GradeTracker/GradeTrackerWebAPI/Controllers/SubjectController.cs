using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing subject-related actions, including retrieving subjects for a student.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SubjectController(ISubjectService entityService, IEntityService<StudentEntity> studentService) : BaseEntityController<SubjectEntity>(entityService)
{
    private readonly ISubjectService _subjectService = entityService;

    /// <summary>
    /// Retrieves the subjects associated with a specific student.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>A list of subjects the student is enrolled in.</returns>
    [HttpGet("get-subjects-for-student")]
    public async Task<ActionResult<List<SubjectEntity>>> GetAssignmentsForStudent(int studentId)
    {
        var foundStudent = await studentService.Get(studentId);

        if (foundStudent == null)
            return NotFound();

        return Ok(foundStudent.Subjects);
    }

    /// <summary>
    /// Retrieves the subject associated with the specified teacher.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher.</param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing the subject assigned to the teacher.
    /// </returns>
    /// <response code="200">Returns the subject associated with the teacher.</response>
    /// <response code="404">If the subject is not found.</response>
    [HttpGet("get-subject-for-teacher{teacherId}")]
    public async Task<ActionResult<List<SubjectEntity>>> GetSubjectForTeacher(int teacherId)
    {
        var subject = await _subjectService.GetSubjectForTeacher(teacherId);

        return Ok(subject);
    }
}
