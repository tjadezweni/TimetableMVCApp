using System.Linq.Expressions;

namespace TimetableMVCApp.Repositories;

public interface IAsyncRepository<TEntity> 
    where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(TEntity entity);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);

    Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression);
}
