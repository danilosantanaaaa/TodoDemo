using TodoApp.Domain.Menus;

namespace TodoApp.Application.Features.Menus.Commands.CreateMenu;

public sealed class MenuCreateCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<MenuCreateCommand, Result<Guid>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menu = Menu.Create(request.Name, request.Icon).Value;
        await _menuRepository.AddAsync(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return menu.Id;
    }
}