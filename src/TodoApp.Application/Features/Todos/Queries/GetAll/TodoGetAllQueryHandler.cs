using MapsterMapper;

using TodoApp.Application.Common.Models;
using TodoApp.Application.Dtos.Todos;

namespace TodoApp.Application.Features.Todos.Queries.GetAll;

public sealed class TodoGetAllQueryHandler(
    ITodoRepository todoRepository,
    IMapper mapper
) : IRequestHandler<TodoGetAllQuery, Result<PaginatedList<TodoResponse>>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PaginatedList<TodoResponse>>> Handle(TodoGetAllQuery request, CancellationToken cancellationToken)
    {
        var pagedList = await _todoRepository.GetAllAsync(request);

        return _mapper.Map<PaginatedList<TodoResponse>>(pagedList);
    }
}