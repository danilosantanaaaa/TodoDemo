using TodoApp.Domain.Todos.Enums;

namespace TodoApp.Domain.Todos.Entities;

public class Kanban : Entity
{
    public string Name { get; private set; }
    public KanbanType Type { get; private set; }
    private Kanban(
        string name,
        KanbanType type,
        Guid? id = null) : base(id ?? Guid.Empty)
    {
        Name = name;
        Type = type;
    }

    public static Result<Kanban> Create(
        string name,
        KanbanType type)
    {
        return new Kanban(name, type);
    }

    public void Update(string name, KanbanType type)
    {
        Name = name;
        Type = type;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Kanban() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}