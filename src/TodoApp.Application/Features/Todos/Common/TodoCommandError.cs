namespace TodoApp.Application.Features.Todos.Common;

public static class TodoCommandError
{
    public static Error NotFound =>
        Error.NotFound(description: "Todo não encontrado");
}