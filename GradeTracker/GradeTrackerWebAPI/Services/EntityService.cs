using Microsoft.EntityFrameworkCore;
using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Services.Interfaces;

public class EntityService<T> : IEntityService<T> where T : class
{
    private readonly GradeTrackerContext _context;

    public EntityService(GradeTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> Get(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<bool> Create(T model)
    {
        await _context.Set<T>().AddAsync(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(T model)
    {
        _context.Set<T>().Update(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
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
