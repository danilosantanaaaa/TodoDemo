
using TodoApp.Domain.Todos.Events;

namespace TodoApp.Application.Features.Todos.Commands.DeleteTodo;

public sealed class TodoDeleteCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<TodoDeleteCommand, Result<Success>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Success>> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id);
        if (todo is null)
        {
            return TodoCommandError.NotFound;
        }

        todo.MarkAsDeleted();
        todo.AddDomain(new TodoDeleted(todo.Id));
        _todoRepository.Update(todo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Success;
    }
}