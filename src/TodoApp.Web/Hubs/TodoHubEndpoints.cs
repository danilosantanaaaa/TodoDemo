using TodoApp.Infrastructure.Features.Todos.Hubs;

namespace TodoApp.Web.Hubs;

public static class TodoHubEndpoints
{
    public static IEndpointRouteBuilder MapTodoHubEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapHub<TodoPendentTotalHub>("hubs/todos/pendentsTotal");

        return app;
    }
}