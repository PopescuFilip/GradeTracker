using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentController(IEntityService<AssignmentEntity> entityService) : BaseEntityController<AssignmentEntity>(entityService)
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAssignmentRequest assignmentRequest)
    {
        var newAssignment = new AssignmentEntity()
        {
            Title = assignmentRequest.Title,
            Description = assignmentRequest.Description,
            SubjectId = assignmentRequest.SubjectId
        };

        var success = await _entityService.Create(newAssignment);

        if (!success)
            return BadRequest();

        return Ok();
    }
}

public record CreateAssignmentRequest(string Title, string Description, int SubjectId);