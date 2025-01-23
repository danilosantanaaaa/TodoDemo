using MapsterMapper;

using TodoApp.Application.Dtos.Menus;

namespace TodoApp.Application.Features.Menus.Queries.GetById;

public sealed class MenuGetByIdQueryHandler(
    IMenuRepository menuRepository,
    IMapper mapper
) : IRequestHandler<MenuGetByIdQuery, Result<MenuResponse>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<MenuResponse>> Handle(MenuGetByIdQuery request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.Id);

        if (menu is null)
        {
            return Error.NotFound(description: "Menu não encontrado.");
        }

        return _mapper.Map<MenuResponse>(menu);
    }
}