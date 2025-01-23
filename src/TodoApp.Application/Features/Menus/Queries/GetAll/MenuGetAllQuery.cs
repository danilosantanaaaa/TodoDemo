using TodoApp.Application.Dtos.Menus;

namespace TodoApp.Application.Features.Menus.Queries.GetAll;

public record MenuGetAllQuery() : IRequest<List<MenuResponse>>;