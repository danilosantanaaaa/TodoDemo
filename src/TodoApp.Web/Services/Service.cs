
using TodoApp.Web.Services.Interfaces;

namespace TodoApp.Web.Services;

public abstract class Service : IService
{
    protected readonly HttpClient _client;
    protected readonly ILogger<Service> _logger;

    public abstract string BaseUri { get; set; }

    public Service(HttpClient client, ILogger<Service> logger)
    {
        _client = client;
        _logger = logger;
    }

    public virtual async Task<List<TModel>?> GetAll<TModel>()
    {
        try
        {
            return await _client.GetFromJsonAsync<List<TModel>?>(BaseUri);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public virtual async Task<TModel?> GetByIdAsync<TModel>(Guid id)
    {
        try
        {
            return await _client.GetFromJsonAsync<TModel?>($"{BaseUri}/{id}");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public virtual async Task<HttpResponseMessage> Post<TModel>(TModel model)
    {
        try
        {
            return await _client.PostAsJsonAsync(BaseUri, model);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public virtual async Task<HttpResponseMessage> Put<TModel>(Guid id, TModel model)
    {
        try
        {
            return await _client.PutAsJsonAsync($"{BaseUri}/{id}", model);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public virtual async Task<HttpResponseMessage> Delete(Guid id)
    {
        try
        {
            return await _client.DeleteAsync($"{BaseUri}/{id}");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}