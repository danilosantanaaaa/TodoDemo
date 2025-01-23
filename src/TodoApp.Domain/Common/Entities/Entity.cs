namespace TodoApp.Domain.Common.Entities;

public abstract class Entity : IAuditable, IEquatable<Entity>
{
    public Guid Id { get; init; }
    public bool IsDeleted { get; private set; } = false;

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime UpdatedOnUtc { get; private set; }

    protected readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id) => Id = id;

    protected Entity() { }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return obj is Entity entity && entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomain(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void MarkAsDeleted() =>
        IsDeleted = true;

    public void SetCreateOn(DateTime dateTime)
    {
        CreatedOnUtc = dateTime;
    }

    public void SetUpdateOn(DateTime dateTime)
    {
        UpdatedOnUtc = dateTime;
    }
}