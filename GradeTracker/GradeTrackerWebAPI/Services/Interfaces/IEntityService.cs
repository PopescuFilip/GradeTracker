using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services.Interfaces
{
    public interface IEntityService<T> where T : Entity
    {
        Task<List<T>> GetAll();

        Task<T?> Get(int id);

        Task<bool> Create(T model);

        Task<bool> Update(T model);

        Task<bool> Delete(int id);
    }
}
