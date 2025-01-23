using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using TodoApp.Application.Dtos.Todos;
using TodoApp.Application.Features.Todos.Commands.CreateKanban;
using TodoApp.Application.Features.Todos.Commands.CreateTodo;
using TodoApp.Application.Features.Todos.Commands.DeleteTodo;
using TodoApp.Application.Features.Todos.Commands.DeleteTodoKanban;
using TodoApp.Application.Features.Todos.Commands.FinishTodo;
using TodoApp.Application.Features.Todos.Commands.UpdateKanban;
using TodoApp.Application.Features.Todos.Commands.UpdateTodo;
using TodoApp.Application.Features.Todos.Queries.GetAll;
using TodoApp.Application.Features.Todos.Queries.GetById;

namespace TodoApp.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodosController(ISender mediator, IMapper mapper) : ControllerBase
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    #region GET

    [HttpGet("all/{menuId:guid}")]
    public async Task<IActionResult> GetAll(Guid menuId, [FromQuery] TodoQueryParameter query)
    {
        TodoGetAllQuery todoGetAllQuery = _mapper.Map<TodoGetAllQuery>((query, menuId));
        var result = await _mediator.Send(todoGetAllQuery);

        return result.MatchFirst(Ok, error => Problem(detail: error.Description));
    }

    [HttpGet("unique/{menuId:guid}/{id:guid}")]
    public async Task<IActionResult> GetById(Guid menuId, Guid id)
    {
        var todoGetById = new TodoGetByIdQuery(menuId, id);
        var result = await _mediator.Send(todoGetById);

        return result.MatchFirst(Ok, error => Problem(detail: error.Description));
    }
    #endregion

    #region POST
    [HttpPost]
    public async Task<IActionResult> AddTodo(TodoRequest request)
    {
        var todoCreateCommand = _mapper.Map<TodoCreateCommand>(request);
        var result = await _mediator.Send(todoCreateCommand);

        return result.MatchFirst(value =>
            CreatedAtAction(nameof(GetById), new { request.MenuId, Id = value }, value),
            error => Problem(detail: error.Description));
    }

    [HttpPost("{todoId:guid}/finish")]
    public async Task<IActionResult> TodoMarkPendentOrFinish(Guid todoId)
    {
        var todoFinishCommand = new TodoFinishCommand(todoId);
        var result = await _mediator.Send(todoFinishCommand);

        return result.MatchFirst<IActionResult>(value => NoContent(), error => Problem(detail: error.Description));
    }

    [HttpPost("{todoId:guid}/kanban")]
    public async Task<IActionResult> AddKanban(TodoKanbanRequest request, Guid todoId)
    {
        var command = _mapper.Map<TodoKanbanCreateCommand>((request, todoId));
        var result = await _mediator.Send(command);

        return result.MatchFirst(value => Ok(value), error => Problem(detail: error.Description));
    }
    #endregion

    #region PUT
    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> UpdateTodo(TodoRequest request, Guid todoId)
    {
        var todoUpdateCommand = _mapper.Map<TodoUpdateCommand>((request, todoId));
        var result = await _mediator.Send(todoUpdateCommand);

        return result.MatchFirst<IActionResult>(
            value => NoContent(),
            error => Problem(detail: error.Description));
    }

    [HttpPut("{todoId:guid}/kanban/{kanbanId:guid}")]
    public async Task<IActionResult> UpdateKaban(TodoKanbanRequest request, Guid todoId, Guid kanbanId)
    {
        var todoKanbanUpdate = _mapper.Map<TodoKanbanUpdateCommand>((request, todoId, kanbanId));
        var result = await _mediator.Send(todoKanbanUpdate);

        return result.MatchFirst<IActionResult>(value => NoContent(), error => Problem(detail: error.Description));
    }
    #endregion

    #region DELETE
    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> DeleteTodo(Guid todoId)
    {
        var todoDeleteCommand = new TodoDeleteCommand(todoId);
        var result = await _mediator.Send(todoDeleteCommand);

        return result.MatchFirst<IActionResult>(value => NoContent(), error => Problem(detail: error.Description));
    }

    [HttpDelete("{todoId:guid}/kanban/{kanbanId:guid}")]
    public async Task<IActionResult> DeleteKanban(Guid todoId, Guid kanbanId)
    {
        var todoDeleteCommand = new TodoKanbanDeleteCommand(todoId, kanbanId);
        var result = await _mediator.Send(todoDeleteCommand);

        return result.MatchFirst<IActionResult>(value => NoContent(), error => Problem(detail: error.Description));
    }
    #endregion
}