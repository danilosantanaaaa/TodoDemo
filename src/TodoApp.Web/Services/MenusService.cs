using TodoApp.Web.Services.Interfaces;

namespace TodoApp.Web.Services;

public class MenusService : Service, IMenusService
{
    public override string BaseUri { get; set; } = "api/menus";

    public MenusService(HttpClient client, ILogger<MenusService> logger) : base(client, logger) { }

}