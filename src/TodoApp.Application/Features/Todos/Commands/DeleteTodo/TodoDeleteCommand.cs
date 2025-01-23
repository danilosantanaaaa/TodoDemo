namespace TodoApp.Application.Features.Todos.Commands.DeleteTodo;

public record TodoDeleteCommand(Guid Id) : IRequest<Result<Success>>;