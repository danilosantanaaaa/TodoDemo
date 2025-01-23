namespace TodoApp.Application.Common.Interfaces.Request;

public interface IPaginatedListRequest<out Request> : IRequest<Request>
{
    int PageSize { get; init; }
    int PageNumber { get; init; }
}