using TodoApp.Application.Common.Enums;
using TodoApp.Application.Common.Interfaces.Request;
using TodoApp.Application.Common.Models;
using TodoApp.Application.Dtos.Todos;

namespace TodoApp.Application.Features.Todos.Queries.GetAll;

public record TodoGetAllQuery(
    Guid MenuId,
    int PageSize,
    int PageNumber,
    string? Description,
    bool? Finished,
    TodoState State) : IPaginatedListRequest<Result<PaginatedList<TodoResponse>>>;
