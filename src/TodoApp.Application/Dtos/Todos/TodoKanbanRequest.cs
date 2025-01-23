using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using TodoApp.Domain.Todos.Enums;

namespace TodoApp.Application.Dtos.Todos;

public class TodoKanbanRequest
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public KanbanType Type { get; set; } = KanbanType.Todo;

    public TodoKanbanRequest() { }

    public TodoKanbanRequest(string name, KanbanType type) : base()
    {
        Name = name;
        Type = type;
    }
}