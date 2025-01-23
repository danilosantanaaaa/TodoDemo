using TodoApp.Domain.Todos;
using TodoApp.Domain.Todos.ValueObjects;

namespace TodoApp.Application.Features.Todos.Commands.CreateTodo;

public sealed class TodoCreateCommandHandler(
    ITodoRepository todoRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<TodoCreateCommand, Result<Guid>>
{
    private readonly ITodoRepository _todoRepository = todoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
    {
        if (Todo.Create(
            request.Description,
            request.UserId,
            request.MenuId,
            request.TimeRemember) is var todoResult && todoResult.IsError)
        {
            return todoResult.Errors;
        }

        var dateRangeResult = DateRange.Create(request.DateStart, request.DateEnd, DateTime.Now);
        if (dateRangeResult.IsError)
        {
            return dateRangeResult.Errors;
        }
        var todo = todoResult.Value;

        if (todo.AddDates(dateRangeResult.Value) is var resultDate && resultDate.IsError)
        {
            return resultDate.Errors;
        }

        await _todoRepository.AddAsync(todo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}