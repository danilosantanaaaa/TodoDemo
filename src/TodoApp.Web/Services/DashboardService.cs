using TodoApp.Application.Dtos.Dashboards;
using TodoApp.Web.Services.Interfaces;

namespace TodoApp.Web.Services;

public class DashboardService(HttpClient client, ILogger<Service> logger) : Service(client, logger), IDashboardService
{
    public override string BaseUri { get; set; } = "api/dashboards";

    public async Task<DashboardResponse> GetDatas()
    {
        try
        {
            return (await _client.GetFromJsonAsync<DashboardResponse>($"{BaseUri}"))!;
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
