namespace TodoApp.Domain.Todos.Events;

public record TodoUpdated(Guid TodoId) : IDomainEvent;