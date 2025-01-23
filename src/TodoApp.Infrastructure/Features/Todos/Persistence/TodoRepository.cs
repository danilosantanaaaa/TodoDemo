using Microsoft.EntityFrameworkCore;


using TodoApp.Application.Common.Interfaces.Persistence;
using TodoApp.Application.Common.Models;
using TodoApp.Application.Features.Todos.Queries.GetAll;
using TodoApp.Domain.Todos;
using TodoApp.Infrastructure.Common.Persistence.Repositories;
using TodoApp.Infrastructure.Features.Todos.Queries;

namespace TodoApp.Infrastructure.Features.Todos.Persistence;

public class TodoRepository : Repository<Todo>, ITodoRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public TodoRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<Todo>> GetAllAsync(Guid menuId)
    {
        return await _dbContext.Todos
            .Where(t => t.MenuId == menuId && !t.Finished)
            .ToListAsync();
    }

    public Task<PaginatedList<Todo>> GetAllAsync(TodoGetAllQuery query)
    {
        IQueryable<Todo> todoQuery = _dbContext.Todos
            .Where(t => t.MenuId == query.MenuId)
            .TodoFilters(query, _dateTimeProvider)
            .OrderByDescending(t => t.DateRange.Start)
            .ThenBy(t => t.Description);
       
       return PaginatedList<Todo>.CreateAsync(todoQuery, query.PageNumber, query.PageSize);
    }

    public async Task<Todo?> GetByIdAsync(Guid menuId, Guid id)
    {
        return await _dbContext.Todos
            .Where(t => t.MenuId == menuId && t.Id == id)
            .FirstOrDefaultAsync();
    }

    private Func<Todo, bool> HasLessThenCurrentDate()
    {
        var dateOnlyNow = DateOnly.FromDateTime(_dateTimeProvider.UtcNow);

        return todo => todo.DateRange.Start != DateOnly.MinValue && todo.DateRange.End != DateOnly.MinValue ?
            dateOnlyNow >= todo.DateRange.Start && dateOnlyNow <= todo.DateRange.End :
            todo.DateRange.Start <= dateOnlyNow;
    }
}