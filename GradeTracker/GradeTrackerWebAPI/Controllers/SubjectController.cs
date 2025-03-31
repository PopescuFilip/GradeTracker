using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing subject-related actions, including retrieving subjects for a student.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SubjectController(IEntityService<SubjectEntity> entityService, IEntityService<StudentEntity> studentService) : BaseEntityController<SubjectEntity>(entityService)
{
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
}
