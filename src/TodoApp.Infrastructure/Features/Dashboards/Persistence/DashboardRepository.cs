using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using TodoApp.Application.Common.Interfaces.Persistence;
using TodoApp.Application.Dtos.Dashboards;

namespace TodoApp.Infrastructure.Features.Dashboards.Persistence;

public class DashboardRepository(
    ApplicationDbContext context,
    IDateTimeProvider dateTimeProvider) : IDashboardRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<DashboardResponse> GetReport()
    {
        var dashboard = new DashboardResponse();
        Stopwatch sw = Stopwatch.StartNew();

        dashboard.MonthlyGroup = await GetTodoReport();
        dashboard.MenuGroup = await GetMenusReport();

        dashboard.ElapsedMilliseconds = sw.ElapsedMilliseconds;
        sw.Stop();

        return dashboard;
    }

    public async Task<List<TodoMonthGroupResponse>> GetTodoReport()
    {

        var todoMonthly = await _context.Todos
            .Where(t =>t.FinishedOnUtc != DateTime.MinValue && t.FinishedOnUtc.Year == DateTime.UtcNow.Year && t.Finished)
            .GroupBy(t => t.FinishedOnUtc.Month)
            .Select(tg => new TodoMonthGroupResponse
            {
                Month = tg.Key,
                Count = tg.Count()
            })
            .ToListAsync();

        for (int i = 1; i <= _dateTimeProvider.UtcNow.Month; i++)
        {
            if (!todoMonthly.Any(x => x.Month == i))
            {
                todoMonthly.Add(new TodoMonthGroupResponse
                {
                    Month = i,
                    Count = 0
                });
            }
        }

        return todoMonthly
            .OrderBy(x => x.Month)
            .ToList();
    }

    public async Task<List<MenuGroupResponse>> GetMenusReport()
    {
        FormattableString querySql = @$"SELECT me.""Id"" as ""MenuId"", me.""Name"", COUNT(*) as ""Count"" FROM ""Todos"" AS td JOIN ""Menus"" AS me
	        ON td.""MenuId"" = me.""Id""
	        WHERE NOT td.""Finished"" AND 
			(
                date_part('year', td.""CreatedOnUtc"" AT TIME ZONE 'UTC')::int = date_part('year', {DateTime.UtcNow} AT TIME ZONE 'UTC')::int OR 
                (td.""CreatedOnUtc"" AT TIME ZONE 'UTC' IS NULL AND {DateTime.UtcNow} AT TIME ZONE 'UTC' IS NULL)
            )
	        GROUP BY me.""Id"""; 

        var todo = await _context.Database.SqlQuery<MenuGroupResponse>(querySql)
        .AsNoTracking()
        .ToListAsync();

        return todo;
    } 
}
