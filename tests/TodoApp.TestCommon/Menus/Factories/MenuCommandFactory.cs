using TodoApp.Application.Features.Menus.Commands.CreateMenu;
using TodoApp.Application.Features.Menus.Commands.UpdateMenu;
using TodoApp.TestCommon.TestUteils.Constaints;

namespace TodoApp.TestCommon.Menus.Factories;

public static class MenuCommandFactory
{
    public static MenuCreateCommand CreateMenuCreateCommand(
        string? name = null,
        string? icon = null)
    {
        return new MenuCreateCommand(
            name ?? Constaint.Menu.Name,
            icon ?? Constaint.Menu.Icon);
    }

    public static MenuUpdateCommand CreateMenuUpdateCommand(
        Guid? menudId = null,
        string? name = null,
        string? icon = null
    )
    {
        return new MenuUpdateCommand(
            menudId ?? Constaint.Menu.Id,
            name ?? Constaint.Menu.Name,
            icon ?? Constaint.Menu.Icon
        );
    }
}