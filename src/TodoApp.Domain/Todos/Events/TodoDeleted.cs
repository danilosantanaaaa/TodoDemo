namespace TodoApp.Domain.Todos.Events;

public record TodoDeleted(Guid TodoId) : IDomainEvent;