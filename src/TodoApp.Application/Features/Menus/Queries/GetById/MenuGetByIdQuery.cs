using TodoApp.Application.Dtos.Menus;

namespace TodoApp.Application.Features.Menus.Queries.GetById;

public record MenuGetByIdQuery(Guid Id) : IRequest<Result<MenuResponse>>;