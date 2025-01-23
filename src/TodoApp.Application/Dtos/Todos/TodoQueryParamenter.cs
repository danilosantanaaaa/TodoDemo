using TodoApp.Application.Common.Enums;

namespace TodoApp.Application.Dtos.Todos;

public class TodoQueryParameter
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string? Description { get; set; } = null;
    public TodoState State { get; set; } = TodoState.Pendent;
}