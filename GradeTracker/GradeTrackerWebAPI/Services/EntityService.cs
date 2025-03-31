using Microsoft.EntityFrameworkCore;
using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Services.Interfaces;
using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services;

/// <summary>
/// Generic service for managing entities.
/// </summary>
/// <typeparam name="T">The entity type, which must derive from <see cref="Entity"/>.</typeparam>
public class EntityService<T>(GradeTrackerContext context) : IEntityService<T> where T : Entity
{
    /// <summary>
    /// The database context.
    /// </summary>
    protected readonly GradeTrackerContext _context = context;

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <param name="includeAllProperties">If <c>true</c>, includes all entity properties.</param>
    /// <returns>A list of entities.</returns>
    public virtual async Task<List<T>> GetAll(bool includeAllProperties = false)
        => await _context.Set<T>().ToListAsync();

    /// <summary>
    /// Retrieves an entity by ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The found entity or <c>null</c> if not found.</returns>
    public virtual async Task<T?> Get(int id)
        => await _context.Set<T>().FindAsync(id);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="model">The model of the entity to create.</param>
    /// <returns><c>true</c> if the operation was successful, otherwise <c>false</c>.</returns>
    public virtual async Task<bool> Create(T model)
    {
        await _context.Set<T>().AddAsync(model);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="model">The model of the entity to update.</param>
    /// <returns><c>true</c> if the operation was successful, otherwise <c>false</c>.</returns>
    public virtual async Task<bool> Update(T model)
    {
        _context.Set<T>().Update(model);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Deletes an entity by ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns><c>true</c> if the operation was successful, otherwise <c>false</c>.</returns>
    public virtual async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        if (entity == null)
        {
            return false;
        }
        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
