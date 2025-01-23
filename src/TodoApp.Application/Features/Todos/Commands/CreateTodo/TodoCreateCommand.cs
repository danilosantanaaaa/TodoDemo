namespace TodoApp.Application.Features.Todos.Commands.CreateTodo;

public record TodoCreateCommand(
    string Description,
    Guid UserId,
    Guid MenuId,
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeOnly? TimeRemember,
    List<string>? Itens) : IRequest<Result<Guid>>;