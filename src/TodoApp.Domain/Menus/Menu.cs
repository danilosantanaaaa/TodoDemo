namespace TodoApp.Domain.Menus;

public class Menu : AggregateRoot
{
    public string Name { get; private set; }
    public string Icon { get; private set; }

    private Menu(
        string name,
        string icon,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Icon = icon;
    }

    public static Result<Menu> Create(
        string name,
        string icon,
        Guid? id = null)
    {
        return new Menu(name, icon, id);
    }

    public Result<Success> Update(string name, string icon)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Name = name;
        }

        if (!string.IsNullOrEmpty(icon))
        {
            Icon = icon;
        }

        return ResultState.Success;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Menu() : base(default)
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}