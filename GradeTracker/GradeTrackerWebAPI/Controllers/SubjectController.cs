using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectController(IEntityService<SubjectEntity> entityService, IEntityService<StudentEntity> studentService) : BaseEntityController<SubjectEntity>(entityService)
{
    [HttpGet("get-subjects-for-student")]
    public async Task<ActionResult<List<SubjectEntity>>> GetAssignmentsForStudent(int studentId)
    {
        var foundStudent = await studentService.Get(studentId);

        if (foundStudent == null)
            return NotFound();

        return Ok(foundStudent.Subjects);
    }
}