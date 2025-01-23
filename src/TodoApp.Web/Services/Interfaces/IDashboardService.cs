using TodoApp.Application.Dtos.Dashboards;

namespace TodoApp.Web.Services.Interfaces;

public interface IDashboardService : IService
{
    Task<DashboardResponse> GetDatas();
};