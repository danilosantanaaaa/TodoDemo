using MudBlazor;

namespace TodoApp.Web.Components.Common.Menus;

public static class MenuExtensions
{

    public static Dictionary<string, (string Check, string UnCheck)> IconMap = new Dictionary<string, (string Check, string UnCheck)>
    {
        { "list", (Icons.Material.Outlined.ViewList, @Icons.Material.Filled.FormatListBulleted) },
        { "sun", (Icons.Material.Filled.WbSunny,Icons.Material.Outlined.WbSunny) },
        { "plan", (Icons.Material.Filled.NextPlan, Icons.Material.Outlined.NextPlan)},
        { "alarm", (Icons.Material.Filled.Favorite, Icons.Material.Outlined.FavoriteBorder)}
    };

    public static string GetIcon(this string icon)
    {
        if (string.IsNullOrEmpty(icon))
        {
            return IconMap["list"].Check;

        }
        var iconTuple = IconMap.GetValueOrDefault(icon.ToLower());
        return iconTuple.Check;
    }
}