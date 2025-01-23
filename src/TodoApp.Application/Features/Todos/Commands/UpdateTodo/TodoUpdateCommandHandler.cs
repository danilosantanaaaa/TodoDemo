using TodoApp.Domain.Todos.ValueObjects;

namespace TodoApp.Application.Features.Todos.Commands.UpdateTodo;

public sealed class TodoUpdateCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<TodoUpdateCommand, Result<Success>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Success>> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id);
        if (todo is null)
        {
            return TodoCommandError.NotFound;
        }

        var dateRangeResult = DateRange.Create(request.DateStart, request.DateEnd, DateTime.Now);
        if (dateRangeResult.IsError)
        {
            return dateRangeResult.Errors;
        }

        if (todo.AddDates(dateRangeResult.Value) is var resultDate && resultDate.IsError)
        {
            return resultDate.Errors;
        }

        todo?.Update(request.Description, request.TimeRemember);

        _todoRepository.Update(todo!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ResultState.Success;
    }
}