using MapsterMapper;

using TodoApp.Application.Dtos.Todos;

namespace TodoApp.Application.Features.Todos.Queries.GetById;

public sealed class TodoGetByIdQueryHandler(
    ITodoRepository todoRepository,
    IMapper mapper
) : IRequestHandler<TodoGetByIdQuery, Result<TodoResponse>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TodoResponse>> Handle(TodoGetByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.MenuId, request.TodoId);
        if (todo is null)
        {
            return Error.NotFound(description: "Tarefa não encontrado.");
        }

        return _mapper.Map<TodoResponse>(todo);
    }
}