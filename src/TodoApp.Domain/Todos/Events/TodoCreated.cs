namespace TodoApp.Domain.Todos.Events;

public record TodoCreated(Guid TodoId) : IDomainEvent;