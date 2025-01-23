namespace TodoApp.Application.Features.Menus.Commands.DeleteMenu;

public sealed class MenuDeleteCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<MenuDeleteCommand, Result<Updated>>
{
    private readonly IMenuRepository _menuRepository = menuRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Updated>> Handle(MenuDeleteCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.Id);
        if (menu is null)
        {
            return Error.NotFound(description: "Menu não encontrado.");
        }

        menu.MarkAsDeleted();
        _menuRepository.Update(menu);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultState.Updated;
    }
}