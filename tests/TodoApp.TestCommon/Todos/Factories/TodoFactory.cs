using TodoApp.Domain.Todos;
using TodoApp.TestCommon.TestUteils.Constaints;

namespace TodoApp.TestCommon.Todos.Factories;

public static class TodoFactory
{
    public static Todo CreateTodo(
        string? description = null,
        Guid? userId = null,
        Guid? menuId = null,
        TimeOnly timeRemember = default,
        Guid? id = null)
    {
        var todo = Todo.Create(
            description ?? Constaint.Todo.Description,
            userId ?? Constaint.Todo.UserId,
            menuId ?? Constaint.Menu.Id,
            timeRemember,
            id ?? Constaint.Todo.TodoId1).Value;
        return todo;
    }
}