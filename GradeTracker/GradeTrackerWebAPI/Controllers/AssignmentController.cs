using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Controller for managing assignment-related actions, such as creating assignments.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AssignmentController(IEntityService<AssignmentEntity> entityService) : BaseEntityController<AssignmentEntity>(entityService)
{
    /// <summary>
    /// Creates a new assignment for a subject.
    /// </summary>
    /// <param name="assignmentRequest">The request data containing the title, description, and subject ID for the assignment.</param>
    /// <returns>A status indicating whether the assignment was successfully created.</returns>
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

/// <summary>
/// Represents the request data for creating a new assignment.
/// </summary>
public record CreateAssignmentRequest(string Title, string Description, int SubjectId);
