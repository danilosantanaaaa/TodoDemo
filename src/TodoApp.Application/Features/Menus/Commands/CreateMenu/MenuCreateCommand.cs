namespace TodoApp.Application.Features.Menus.Commands.CreateMenu;

public record MenuCreateCommand(
    string Name,
    string Icon) : IRequest<Result<Guid>>;