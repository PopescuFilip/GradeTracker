using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing student-related actions, including retrieving students for a subject.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StudentController(IEntityService<StudentEntity> entityService, IEntityService<SubjectEntity> subjectService) : BaseEntityController<StudentEntity>(entityService)
{
    /// <summary>
    /// Retrieves the students enrolled in a specific subject.
    /// </summary>
    /// <param name="subjectId">The ID of the subject.</param>
    /// <returns>A list of students enrolled in the specified subject.</returns>
    [HttpGet("get-students-for-subject")]
    public async Task<ActionResult<List<StudentEntity>>> GetStudentsForSubject(int subjectId)
    {
        var foundSubject = await subjectService.Get(subjectId);

        if (foundSubject == null)
            return NotFound();

        return Ok(foundSubject.Students);
    }
}
