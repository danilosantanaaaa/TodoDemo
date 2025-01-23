using TodoApp.Domain.Todos.Entities;
using TodoApp.Domain.Todos.Events;
using TodoApp.Domain.Todos.ValueObjects;

namespace TodoApp.Domain.Todos;

public class Todo : AggregateRoot
{
    private readonly List<Kanban> _kanBanItens = new();
    private readonly Guid _menuId;
    private readonly Guid _userId;
    public string Description { get; private set; }
    public bool Finished { get; private set; }
    public DateTime FinishedOnUtc { get; private set; }
    public DateRange DateRange { get; private set; }
    public TimeOnly? TimeRemember { get; private set; }
    public Guid UserId => _userId;
    public Guid MenuId => _menuId;
    public IReadOnlyList<Kanban> Kanbuns => _kanBanItens.ToList();

    private Todo(
        string description,
        Guid userId,
        Guid menuId,
        TimeOnly? timeRemember,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Description = description;
        Finished = false;
        _userId = userId;
        _menuId = menuId;
        TimeRemember = timeRemember;
        DateRange = DateRange.CreateEmpty();
    }

    public static Result<Todo> Create(
        string description,
        Guid userId,
        Guid menuId,
        TimeOnly? timeRemember,
        Guid? id = null)
    {
        var todo = new Todo(
            description,
            userId,
            menuId,
            timeRemember,
            id);

        todo.AddDomain(new TodoCreated(todo.Id));

        return todo;
    }

    public Result<Updated> Update(
        string description,
        TimeOnly? timeRemember)
    {
        if (Finished)
        {
            return TodoError.NotAllowWhenFinished;
        }

        Description = description;
        TimeRemember = timeRemember;

        AddDomain(new TodoUpdated(Id));

        return ResultState.Updated;
    }

    public Result<Success> MarkAsFinish(DateTime dateTimeUtc)
    {
        Finished = true;
        FinishedOnUtc = dateTimeUtc;
        AddDomain(new TodoFinished());
        return ResultState.Success;
    }

    public Result<Success> MarkAsPendent()
    {
        Finished = false;
        FinishedOnUtc = DateTime.MinValue;
        AddDomain(new TodoFinished());
        return ResultState.Success;
    }

    public Result<Success> AddDates(
        DateRange dateRange)
    {
        if (Finished)
        {
            return TodoError.NotAllowWhenFinished;
        }

        DateRange = dateRange;

        return ResultState.Success;
    }

    public Result<Success> AddKanbanItem(Kanban kanban)
    {
        _kanBanItens.Add(kanban);
        return ResultState.Success;
    }

    public Result<Deleted> RemoveKanbanItem(Guid kanbanId)
    {
        var kanban = _kanBanItens.Find(k => k.Id == kanbanId);
        if (kanban is null)
        {
            return TodoError.KanbanNotFound;
        }

        _kanBanItens.Remove(kanban);

        return ResultState.Deleted;
    }

    public Result<Kanban> GetKanban(Guid kanbanId)
    {
        var kanban = _kanBanItens.Find(k => k.Id == kanbanId);
        if (kanban is null)
        {
            return TodoError.KanbanNotFound;
        }

        return kanban;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.    
    protected Todo() : base(default)
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}