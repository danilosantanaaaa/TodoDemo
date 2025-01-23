using TodoApp.Application.Dtos.Dashboards;

namespace TodoApp.Application.Common.Interfaces.Persistence;

public interface IDashboardRepository
{
    Task<DashboardResponse> GetReport();
}
