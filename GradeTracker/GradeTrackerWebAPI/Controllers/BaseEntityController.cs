using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

/// <summary>
/// Base controller class providing common CRUD operations for entities.
/// </summary>
/// <typeparam name="T">The type of the entity this controller manages.</typeparam>
public abstract class BaseEntityController<T>(IEntityService<T> entityService) : ControllerBase where T : Entity
{
    protected readonly IEntityService<T> _entityService = entityService;

    /// <summary>
    /// Retrieves all entities of type T.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    [HttpGet("get-all")]
    public async Task<ActionResult<List<T>>> GetAll()
    {
        var entities = await _entityService.GetAll();
        return Ok(entities);
    }

    /// <summary>
    /// Retrieves a single entity of type T by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity if found; otherwise, a not found status.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id)
    {
        var entity = await _entityService.Get(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    /// <summary>
    /// Updates an existing entity of type T.
    /// </summary>
    /// <param name="id">The ID of the entity to update.</param>
    /// <param name="model">The updated entity data.</param>
    /// <returns>A status indicating whether the update was successful.</returns>
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

    /// <summary>
    /// Deletes an entity of type T by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A status indicating whether the deletion was successful.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _entityService.Delete(id);

        if (!success)
            return NotFound();

        return Ok();
    }
}
