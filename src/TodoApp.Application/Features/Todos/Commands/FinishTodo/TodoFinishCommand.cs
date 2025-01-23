namespace TodoApp.Application.Features.Todos.Commands.FinishTodo;

public record TodoFinishCommand(Guid TodoId) : IRequest<Result<Success>>;