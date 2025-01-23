using TodoApp.Domain.Menus;
using TodoApp.TestCommon.TestUteils.Constaints;

namespace TodoApp.TestCommon.Menus.Factories;

public static class MenuFactory
{
    public static Menu CreateMenu(
        string? name = null,
        string? icon = null,
        Guid? id = null
    )
    {
        return Menu.Create(
            name ?? Constaint.Menu.Name,
            icon ?? Constaint.Menu.Icon,
            id ?? Constaint.Menu.Id
        ).Value;
    }
}