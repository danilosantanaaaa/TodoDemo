namespace TodoApp.Domain.Todos;

public static class TodoError
{
    public static Error NotAllowWhenFinished =>
        Error.Validation("Todo.NotAllowWhenFinished", "Não é permitido editar, após marcar como concluído.");

    public static Error KanbanNotFound =>
        Error.NotFound("Todo.KanbanNotFound", "O Item não foi encontrado.");
}