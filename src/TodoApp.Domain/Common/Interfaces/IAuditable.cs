namespace TodoApp.Domain.Common.Interfaces;

public interface IAuditable
{
    public DateTime CreatedOnUtc { get; }
    public DateTime UpdatedOnUtc { get; }

    public void SetCreateOn(DateTime dateTime);
    public void SetUpdateOn(DateTime dateTime);
}