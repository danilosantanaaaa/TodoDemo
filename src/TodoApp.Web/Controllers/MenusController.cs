using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TodoApp.Application.Dtos.Menus;
using TodoApp.Application.Features.Menus.Commands.CreateMenu;
using TodoApp.Application.Features.Menus.Commands.DeleteMenu;
using TodoApp.Application.Features.Menus.Commands.UpdateMenu;
using TodoApp.Application.Features.Menus.Queries.GetAll;
using TodoApp.Application.Features.Menus.Queries.GetById;

namespace TodoApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenusController(ISender mediator, IMapper mapper) : ControllerBase
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menuAllQuery = new MenuGetAllQuery();
        var result = await _mediator.Send(menuAllQuery);

        return Ok(result);
    }

    [HttpGet("{menuId:guid}")]
    public async Task<IActionResult> GetById(Guid menuId)
    {
        var menuGetByIdQuery = new MenuGetByIdQuery(menuId);
        var result = await _mediator.Send(menuGetByIdQuery);

        return result.MatchFirst(Ok, error => Problem(detail: error.Description));
    }

    [HttpPost]
    public async Task<IActionResult> Post(MenuRequest request)
    {
        var menuCreateCommand = _mapper.Map<MenuCreateCommand>(request);
        var result = await _mediator.Send(menuCreateCommand);

        return result.MatchFirst(value =>
            CreatedAtAction(nameof(GetById), new { MenuId = value }, value),
            error => Problem(detail: error.Description));
    }

    [HttpPut("{menuId:guid}")]
    public async Task<IActionResult> Put(MenuRequest request, Guid menuId)
    {
        var menuUpdateCommand = new MenuUpdateCommand(menuId, request.Name, request.Icon);
        var result = await _mediator.Send(menuUpdateCommand);
        return result.MatchFirst<IActionResult>(
            value => NoContent(),
            error => Problem(detail: error.Description));
    }

    [HttpDelete("{menuId:guid}")]
    public async Task<IActionResult> Delete(Guid menuId)
    {
        var menuDeleteCommand = new MenuDeleteCommand(menuId);
        var result = await _mediator.Send(menuDeleteCommand);
        return result.MatchFirst<IActionResult>(
            value => NoContent(),
            error => Problem(detail: error.Description));
    }

}