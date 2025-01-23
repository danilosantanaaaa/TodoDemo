namespace TodoApp.Application.Features.Todos.Commands.DeleteTodoKanban;

public record TodoKanbanDeleteCommand(
    Guid TodoId,
    Guid KanbanId) : IRequest<Result<Deleted>>;