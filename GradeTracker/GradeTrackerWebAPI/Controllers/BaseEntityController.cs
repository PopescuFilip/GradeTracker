using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

public abstract class BaseEntityController<T>(IEntityService<T> entityService) : ControllerBase where T : Entity
{
    protected readonly IEntityService<T> _entityService = entityService;

    [HttpGet("get-all")]
    public async Task<ActionResult<List<T>>> GetAll()
    {
        var entities = await _entityService.GetAll();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id)
    {
        var entity = await _entityService.Get(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] T model)
    {
        if (id != model.Id)
            return BadRequest("ID mismatch");

        var success = await _entityService.Update(model);

        if (!success)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _entityService.Delete(id);

        if (!success)
            return NotFound();

        return Ok();
    }
}