namespace TodoApp.Infrastructure.Features.Todos.Hubs;

public class TodoPendentTotalHub(ITodoServiceSignalR todoService) : Hub<ITodoPendentTotalClient>
{
    public override async Task OnConnectedAsync()
    {
        await todoService.TotalPendentNotifier();
        await base.OnConnectedAsync();
    }
}

public interface ITodoPendentTotalClient
{
    Task ReceiveTodoPendentTotal(Dictionary<Guid, int> message);
}