using TodoApp.Domain.Todos.Enums;

namespace TodoApp.Application.Dtos.Todos;

public class TodoResponse : Response
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public bool Finished { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public TimeOnly? TimeRemember { get; set; }
    public Guid MenuId { get; set; }
    public List<TodoKanbanResponse> KanbansResponse { get; set; } = new();
}

public class TodoKanbanResponse : Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public KanbanType Type { get; set; }
}
