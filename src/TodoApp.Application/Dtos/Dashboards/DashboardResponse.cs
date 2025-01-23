namespace TodoApp.Application.Dtos.Dashboards;

public class DashboardResponse
{
    public List<TodoMonthGroupResponse> MonthlyGroup { get; set; } = new();
    public List<MenuGroupResponse> MenuGroup { get; set; } = new();
    public long ElapsedMilliseconds { get; set; }
}

public class TodoMonthGroupResponse
{
    public int Month { get; set; }
    public int Count { get; set; }
}

public class MenuGroupResponse
{
    public Guid MenuId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}
