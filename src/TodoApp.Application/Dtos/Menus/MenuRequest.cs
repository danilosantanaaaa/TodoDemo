using System.ComponentModel.DataAnnotations;

namespace TodoApp.Application.Dtos.Menus;

public class MenuRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Icon { get; set; } = "list";
}