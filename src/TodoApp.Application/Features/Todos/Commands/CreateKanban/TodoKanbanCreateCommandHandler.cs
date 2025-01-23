
using TodoApp.Domain.Todos.Entities;

namespace TodoApp.Application.Features.Todos.Commands.CreateKanban;

public sealed class TodoKanbanCreateCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<TodoKanbanCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(TodoKanbanCreateCommand request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetByIdAsync(request.TodoId);
        if (todo is null)
        {
            return TodoCommandError.NotFound;
        }

        var kanbanResult = Kanban.Create(
            request.Name,
            request.KanbanType);

        if (kanbanResult.IsError)
        {
            return kanbanResult.Errors;
        }

        todo.AddKanbanItem(kanbanResult.Value);

        todoRepository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return kanbanResult.Value.Id;
    }
}
