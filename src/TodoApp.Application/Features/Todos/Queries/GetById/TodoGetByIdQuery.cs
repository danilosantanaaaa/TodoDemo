using TodoApp.Application.Dtos.Todos;

namespace TodoApp.Application.Features.Todos.Queries.GetById;

public record TodoGetByIdQuery(Guid MenuId, Guid TodoId) : IRequest<Result<TodoResponse>>;