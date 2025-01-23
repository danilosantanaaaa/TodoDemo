
using System.Linq.Expressions;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TodoApp.Domain.Todos;
using TodoApp.Infrastructure.Features.Todos.Hubs;
using TodoApp.Infrastructure.Features.Todos.Queries;

namespace TodoApp.Infrastructure.Features.Todos.Services;

public class TodoServiceSignalR(
    ApplicationDbContext context,
    ILogger<TodoServiceSignalR> logger,
    IHubContext<TodoPendentTotalHub, ITodoPendentTotalClient> hubContext,
    IDateTimeProvider dateTimeProvider) : ITodoServiceSignalR
{
    public async Task TotalPendentNotifier()
    {
        logger.LogInformation("Preparando dados de tarefas pendente para enviar via SignalR...");

        var todoGroup = await context.Todos
                .Where(IsPendent())
                .WhereLessThenCurrentDate(dateTimeProvider)
                .GroupBy(t => t.MenuId)
                .ToDictionaryAsync(t => t.Key, t => t.Count());

        var datetime = DateTime.Now;
        await hubContext.Clients.All.ReceiveTodoPendentTotal(todoGroup);

        logger.LogInformation($"Notificação enviada em {datetime}. {todoGroup.Count} enviado.");
    }

    private Expression<Func<Todo, bool>> IsPendent() =>
        todo => !todo.Finished;
}