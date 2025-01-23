namespace TodoApp.Application.Features.Menus.Commands.DeleteMenu;

public record MenuDeleteCommand(Guid Id) : IRequest<Result<Updated>>;