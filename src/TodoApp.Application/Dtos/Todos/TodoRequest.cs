using System.ComponentModel.DataAnnotations;

namespace TodoApp.Application.Dtos.Todos;

public class TodoRequest
{
    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid MenuId { get; set; }

    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public TimeSpan? TimeRemember { get; set; }
}