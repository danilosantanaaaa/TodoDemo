using TodoApp.Application.Common.Models;
using TodoApp.Application.Dtos.Todos;
using TodoApp.Web.Common.Http;
using TodoApp.Web.Services.Interfaces;

namespace TodoApp.Web.Services;

public class TodoService : Service, ITodoService
{
    public TodoService(HttpClient client, ILogger<TodoService> logger) : base(client, logger)
    { }

    public override string BaseUri { get; set; } = "api/todos";

    public async Task<PaginatedList<TModel>?> GetAll<TModel>(Guid menuId, TodoQueryParameter queryParamenter)
    {
        try
        {
            var queryParameterEncode = new HttpQueryStringBuilder()
                    .AddQuery(queryParamenter)
                    .Build();

           return (await _client.GetPaginatedListFromJsonAsync<TModel>($"{BaseUri}/all/{menuId}{queryParameterEncode}"))!;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<TModel?> GetByIdAsync<TModel>(Guid menuId, Guid id)
    {
        try
        {
            return await _client.GetFromJsonAsync<TModel>($"{BaseUri}/unique/{menuId}/{id}");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> MarkPendentOrFinish(Guid todoId)
    {
        try
        {
            return await _client.PostAsync($"{BaseUri}/{todoId}/finish", null);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> AddTodoKanban<TModel>(Guid todoId, TModel kanban)
    {
        try
        {
            return await _client.PostAsJsonAsync($"{BaseUri}/{todoId}/kanban", kanban);
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> UpdateTodoKanban<TModel>(Guid todoId, Guid kanbanId, TModel kanban)
    {
        try
        {
            return await _client.PutAsJsonAsync($"{BaseUri}/{todoId}/kanban/{kanbanId}", kanban);
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> DeleteTodoKanban(Guid todoId, Guid kanbanId)
    {
        try
        {
            return await _client.DeleteAsync($"{BaseUri}/{todoId}/kanban/{kanbanId}");
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}