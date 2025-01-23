namespace TodoApp.Application.Features.Menus.Commands.UpdateMenu;

public sealed class MenuUpdateCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<MenuUpdateCommand, Result<Deleted>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Deleted>> Handle(MenuUpdateCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.Id);
        if (menu is null)
        {
            return Error.NotFound(description: "Menu não encontrado.");
        }

        menu.Update(request.Name, request.Icon);
        _menuRepository.Update(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Deleted;
    }
}