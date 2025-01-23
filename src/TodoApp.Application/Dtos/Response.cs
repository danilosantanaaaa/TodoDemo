namespace TodoApp.Application.Dtos;

public abstract class Response
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}