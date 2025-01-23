namespace TodoApp.Application.Features.Menus.Commands.UpdateMenu;

public record MenuUpdateCommand(
    Guid Id,
    string Name,
    string Icon) : IRequest<Result<Deleted>>;