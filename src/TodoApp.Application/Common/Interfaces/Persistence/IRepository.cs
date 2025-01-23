using TodoApp.Application.Common.Models;

namespace TodoApp.Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<PaginatedList<TEntity>> GetAllAsync(int pageNumber, int pageSize);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}