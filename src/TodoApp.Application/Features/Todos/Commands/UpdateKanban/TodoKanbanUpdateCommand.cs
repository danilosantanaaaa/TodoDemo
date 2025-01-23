using TodoApp.Domain.Todos.Enums;

namespace TodoApp.Application.Features.Todos.Commands.UpdateKanban;

public record TodoKanbanUpdateCommand(
    Guid TodoId,
    Guid KanbanId,
    string Name,
    KanbanType Type) : IRequest<Result<Updated>>;