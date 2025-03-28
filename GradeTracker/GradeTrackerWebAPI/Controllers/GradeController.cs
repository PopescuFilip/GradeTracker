﻿using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController(IGradeService gradeService) : BaseEntityController<GradeEntity>(gradeService)
{
    private readonly IGradeService _gradeService = gradeService;

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

    [HttpGet("get-grades-for-subject-and-student/{subjectId}/{studentId}")]
    public async Task<ActionResult<List<GradeEntity>>> GetGradesForSubjectAndStudent(int subjectId, int studentId)
    {
        var grades = await _gradeService.GetGradesForSubjectAndStudent(subjectId, studentId);

        if (grades == null)
            return NotFound();

        return Ok(grades);
    }
}

public record CreateGradeRequest(int Grade, int StudentId, int AssignmentId);