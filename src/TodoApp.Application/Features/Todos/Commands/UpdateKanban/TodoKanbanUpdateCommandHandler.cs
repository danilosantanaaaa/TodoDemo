
namespace TodoApp.Application.Features.Todos.Commands.UpdateKanban;

public sealed class TodoKanbanUpdateCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<TodoKanbanUpdateCommand, Result<Updated>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Updated>> Handle(TodoKanbanUpdateCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.TodoId);
        if (todo is null)
        {
            return TodoCommandError.NotFound;
        }

        var kanbanResult = todo.GetKanban(request.KanbanId);
        if (kanbanResult.IsError)
        {
            return kanbanResult.Errors;
        }

        var kanban = kanbanResult.Value;

        kanban.Update(request.Name, request.Type);
        _todoRepository.Update(todo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Updated;
    }
}
