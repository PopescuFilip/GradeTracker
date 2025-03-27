using Microsoft.EntityFrameworkCore;
using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Services.Interfaces;
using GradeTrackerWebAPI.Models;

namespace GradeTrackerWebAPI.Services;

public class EntityService<T>(GradeTrackerContext context) : IEntityService<T> where T : Entity
{
    protected readonly GradeTrackerContext _context = context;

    public virtual async Task<List<T>> GetAll(bool includeAllProperties = false)
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> Get(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<bool> Create(T model)
    {
        await _context.Set<T>().AddAsync(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> Update(T model)
    {
        _context.Set<T>().Update(model);
        return await _context.SaveChangesAsync() > 0;
    }

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