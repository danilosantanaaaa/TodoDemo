using TodoApp.Application.Common.Models;
using TodoApp.Application.Features.Todos.Queries.GetAll;
using TodoApp.Domain.Todos;

namespace TodoApp.Application.Common.Interfaces.Persistence;

public interface ITodoRepository : IRepository<Todo>
{
    Task<List<Todo>> GetAllAsync(Guid menuId);
    Task<Todo?> GetByIdAsync(Guid menuId, Guid id);
    Task<PaginatedList<Todo>> GetAllAsync(TodoGetAllQuery query);
}