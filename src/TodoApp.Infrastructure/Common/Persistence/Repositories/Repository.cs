using Microsoft.EntityFrameworkCore;

using TodoApp.Application.Common.Interfaces.Persistence;
using TodoApp.Application.Common.Models;

namespace TodoApp.Infrastructure.Common.Persistence.Repositories;

public abstract class Repository<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext
            .Set<TEntity>()
            .AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbContext
            .Set<TEntity>()
            .Remove(entity);
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _dbContext
            .Set<TEntity>()
            .ToListAsync();
    }

    public virtual async Task<PaginatedList<TEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await PaginatedList<TEntity>.CreateAsync(
            _dbContext.Set<TEntity>(),
            pageNumber,
            pageSize);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext
            .Set<TEntity>()
            .FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        _dbContext
            .Set<TEntity>()
            .Update(entity);
    }
}