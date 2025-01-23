
namespace TodoApp.Application.Features.Todos.Commands.FinishTodo;

public class TodoFinishCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider
) : IRequestHandler<TodoFinishCommand, Result<Success>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Success>> Handle(TodoFinishCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.TodoId);
        if (todo is null)
        {
            return Error.NotFound(description: "Todo não encontrado.");
        }

        if (todo.Finished)
        {
            todo.MarkAsPendent();
        }
        else
        {
            todo.MarkAsFinish(dateTimeProvider.UtcNow);
        }

        _todoRepository.Update(todo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Success;
    }
}