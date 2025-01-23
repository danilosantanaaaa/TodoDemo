using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TodoApp.Application.Features.Dashboard.GetDatas;

namespace TodoApp.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardsController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetDatas()
    {
        var dashboardGetDatas = new DashboardGetDatas();
        var result = await _mediator.Send(dashboardGetDatas);

        return Ok(result);
    }
}
