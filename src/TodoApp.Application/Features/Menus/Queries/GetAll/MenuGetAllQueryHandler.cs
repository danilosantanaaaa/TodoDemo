using MapsterMapper;

using TodoApp.Application.Dtos.Menus;

namespace TodoApp.Application.Features.Menus.Queries.GetAll;

public sealed class MenuGetAllQueryHandler(
    IMenuRepository menuRepository,
    IMapper mapper
) : IRequestHandler<MenuGetAllQuery, List<MenuResponse>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<MenuResponse>> Handle(MenuGetAllQuery request, CancellationToken cancellationToken)
    {
        var menus = await _menuRepository.GetAllAsync();
        return _mapper.Map<List<MenuResponse>>(menus);
    }
}