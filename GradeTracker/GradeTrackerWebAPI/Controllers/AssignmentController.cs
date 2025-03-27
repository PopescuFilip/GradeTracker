using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentController(IEntityService<AssignmentEntity> entityService, IEntityService<StudentEntity> studentService) : BaseEntityController<AssignmentEntity>(entityService)
{
    [HttpGet("get-assignments-for-student")]
    public async Task<ActionResult<List<AssignmentEntity>>> GetAssignmentsForStudent(int studentId)
    {
        var foundStudent = await studentService.Get(studentId);

        if (foundStudent == null)
            return NotFound();

        return Ok(foundStudent);
    }
}