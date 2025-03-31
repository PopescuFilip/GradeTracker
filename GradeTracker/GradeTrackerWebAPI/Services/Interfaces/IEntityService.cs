using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    /// <summary>
    /// Generic interface for managing entities.
    /// </summary>
    /// <typeparam name="T">The entity type, which must inherit from <see cref="Entity"/>.</typeparam>
    public interface IEntityService<T> where T : Entity
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <param name="includeAllProperties">Indicates whether to include all related properties.</param>
        /// <returns>A list of entities of type <see cref="T"/>.</returns>
        Task<List<T>> GetAll(bool includeAllProperties = false);

        /// <summary>
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>The entity of type <see cref="T"/> if found, otherwise <c>null</c>.</returns>
        Task<T?> Get(int id);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="model">The entity to create.</param>
        /// <returns><c>true</c> if the creation was successful, otherwise <c>false</c>.</returns>
        Task<bool> Create(T model);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="model">The entity to update.</param>
        /// <returns><c>true</c> if the update was successful, otherwise <c>false</c>.</returns>
        Task<bool> Update(T model);

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns><c>true</c> if the deletion was successful, otherwise <c>false</c>.</returns>
        Task<bool> Delete(int id);
    }

}
