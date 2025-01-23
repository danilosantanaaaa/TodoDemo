namespace TodoApp.Application.Features.Todos.Commands.UpdateTodo;

public record TodoUpdateCommand(
    Guid Id,
    string Description,
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeOnly? TimeRemember,
    List<string>? Itens) : IRequest<Result<Success>>;