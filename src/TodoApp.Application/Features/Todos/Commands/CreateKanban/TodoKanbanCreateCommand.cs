using TodoApp.Domain.Todos.Enums;

namespace TodoApp.Application.Features.Todos.Commands.CreateKanban;

public record TodoKanbanCreateCommand(
    Guid TodoId,
    string Name,
    KanbanType KanbanType) : IRequest<Result<Guid>>;