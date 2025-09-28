using System.Linq.Expressions;
using DotNetLab.Data.Context;
using DotNetLab.Data.Repositories.Interfaces;
using DotNetLab.Model.Framework;
using Microsoft.EntityFrameworkCore;

namespace DotNetLab.Data.Repositories.Implementation;

public class Repository<T>(ApplicationDbContext context): IRepository<T>
    where T : DatabaseEntity
{
    protected readonly DbSet<T> DbSet = context.Set<T>();
    //  TODO: Replace once Identity management is in place
    private readonly Guid _adminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private readonly Guid _systemUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await DbSet.Where(expression).ToListAsync();
    }

    public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await DbSet.SingleOrDefaultAsync(expression);
    }

    public virtual async Task AddAsync(T entity)
    {
        SetAuditFields(entity, isUpdate: false);
        await DbSet.AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            SetAuditFields(entity, isUpdate: false);
        }
        await DbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        SetAuditFields(entity, isUpdate: true);
        DbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            SetAuditFields(entity, isUpdate: true);
        }
        DbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.AnyAsync(e => e.Id == id);
    }

    public virtual async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await DbSet.CountAsync(expression);
    }

    private void SetAuditFields(T entity, bool isUpdate)
    {
        var now = DateTime.UtcNow;

        if (!isUpdate)
        {
            entity.CreatedDateTime = now;
            entity.CreatedBy = _systemUserId;
        }

        entity.LastModifiedDateTime = now;
        entity.LastModifiedBy = _systemUserId;
    }
}
