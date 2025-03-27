using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController(IEntityService<StudentEntity> entityService, IEntityService<SubjectEntity> subjectService) : BaseEntityController<StudentEntity>(entityService)
{
    [HttpGet("get-students-for-subject")]
    public async Task<ActionResult<List<StudentEntity>>> GetStudentsForClass(int subjectId)
    {
        var foundSubject = await subjectService.Get(subjectId);

        if (foundSubject == null)
            return NotFound();

        return Ok(foundSubject.Students);
    }
}