
namespace TodoApp.Application.Features.Todos.Commands.DeleteTodoKanban;

public sealed class TodoKanbanDeleteCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<TodoKanbanDeleteCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(TodoKanbanDeleteCommand request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetByIdAsync(request.TodoId);
        if (todo is null)
        {
            return TodoCommandError.NotFound;
        }

        var result = todo.RemoveKanbanItem(request.KanbanId);
        if (result.IsError)
        {
            return result.Errors;
        }

        todoRepository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Deleted;
    }
}
