using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;

namespace TimetableMVCApp.Repositories.Impl;

public class BaseRepository<TEntity> : IAsyncRepository<TEntity>
    where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(SchoolDBContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return await Task.FromResult(true);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        TEntity? entity = await _dbSet.FirstOrDefaultAsync(expression);
        return entity;
    }

    public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return await Task.FromResult(entity);
    }
}
