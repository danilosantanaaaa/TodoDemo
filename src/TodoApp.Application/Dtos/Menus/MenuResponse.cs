namespace TodoApp.Application.Dtos.Menus;

public class MenuResponse : Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}