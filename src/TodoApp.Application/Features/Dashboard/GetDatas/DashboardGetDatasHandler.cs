using TodoApp.Application.Dtos.Dashboards;

namespace TodoApp.Application.Features.Dashboard.GetDatas;

public sealed class DashboardGetDatasHandler(IDashboardRepository dashboardRepository) : IRequestHandler<DashboardGetDatas, DashboardResponse>
{
    private readonly IDashboardRepository _dashboardRepository = dashboardRepository;

    public async Task<DashboardResponse> Handle(DashboardGetDatas request, CancellationToken cancellationToken)
    {
        return await _dashboardRepository.GetReport();
    }
}
