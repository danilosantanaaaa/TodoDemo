using TodoApp.Application.Common.Models;
using TodoApp.Application.Dtos.Todos;

namespace TodoApp.Web.Services.Interfaces;

public interface ITodoService : IService
{
    Task<PaginatedList<TModel>?> GetAll<TModel>(Guid menuId, TodoQueryParameter queryParamenter);
    Task<TModel?> GetByIdAsync<TModel>(Guid menuId, Guid id);
    Task<HttpResponseMessage> MarkPendentOrFinish(Guid todoId);
    Task<HttpResponseMessage> AddTodoKanban<TModel>(Guid todoId, TModel kanban);
    Task<HttpResponseMessage> UpdateTodoKanban<TModel>(Guid todoId, Guid kanbanId, TModel kanban);
    Task<HttpResponseMessage> DeleteTodoKanban(Guid todoId, Guid kanbanId);
}