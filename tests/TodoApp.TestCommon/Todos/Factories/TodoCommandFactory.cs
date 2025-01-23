using TodoApp.Application.Features.Todos.Commands.CreateTodo;
using TodoApp.Application.Features.Todos.Commands.UpdateTodo;
using TodoApp.TestCommon.TestUteils.Constaints;

namespace TodoApp.TestCommon.Todos.Factories;

public static class TodoCommandFactory
{
    public static TodoCreateCommand CreateTodoCreateCommand(
        string? description = null,
        Guid? userId = null,
        Guid? menuId = null,
        DateOnly dateStart = default,
        DateOnly dateEnd = default,
        TimeOnly timeRemember = default,
        List<string>? itens = null)
    {
        return new TodoCreateCommand(
            description ?? Constaint.Todo.Description,
            userId ?? Constaint.Todo.UserId,
            menuId ?? Constaint.Menu.Id,
            dateStart,
            dateEnd,
            timeRemember,
            itens);
    }

    public static TodoUpdateCommand CreateTodoUpdateCommand(
        string? description = null,
        Guid? todoId = null,
        DateOnly dateStart = default,
        DateOnly dateEnd = default,
        TimeOnly timeRemember = default,
        List<string>? itens = null)
    {
        return new TodoUpdateCommand(
            todoId ?? Constaint.Todo.TodoId1,
            description ?? Constaint.Todo.Description,
            dateStart,
            dateEnd,
            timeRemember,
            itens);
    }
}