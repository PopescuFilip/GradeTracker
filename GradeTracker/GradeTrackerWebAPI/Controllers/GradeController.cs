using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController(IGradeService gradeService) : BaseEntityController<GradeEntity>(gradeService)
{
    private readonly IGradeService _gradeService = gradeService;

    [HttpGet("get-grades-for-subject-and-student/{subjectId}/{studentId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
    {
        var grades = await _gradeService.GetGradesForSubjectAndStudent(subjectId, studentId);

        if (grades == null)
            return NotFound();

        return Ok(grades);
    }
}