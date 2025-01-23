using TodoApp.Application.Common.Enums;
using TodoApp.Application.Features.Todos.Queries.GetAll;
using TodoApp.Domain.Todos;

namespace TodoApp.Infrastructure.Features.Todos.Queries;

internal static class TodoQueries
{
    public static IQueryable<Todo> WhereLessThenCurrentDate(this IQueryable<Todo> query, IDateTimeProvider dateTimeProvider)
    {
        var dateOnlyNow = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        return query.Where(t => t.DateRange.Start != DateOnly.MinValue && t.DateRange.End != DateOnly.MinValue ?
            dateOnlyNow >= t.DateRange.Start && dateOnlyNow <= t.DateRange.End :
            t.DateRange.Start <= dateOnlyNow);
    }

    public static IQueryable<Todo> TodoFilters(this IQueryable<Todo> query, TodoGetAllQuery queryParamenter, IDateTimeProvider dateTimeProvider)
    {
        var dateOnlyNow = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        return query
            .Where(t =>
                queryParamenter.State == TodoState.Pendent ? !t.Finished &&
                    ( 
                        t.DateRange.Start != DateOnly.MinValue && t.DateRange.End != DateOnly.MinValue ?
                        dateOnlyNow >= t.DateRange.Start && dateOnlyNow <= t.DateRange.End : 
                        t.DateRange.Start <= dateOnlyNow
                    ) :
                queryParamenter.State == TodoState.Finished ?
                    t.Finished :
                queryParamenter.State == TodoState.Non_Finished ?
                    !t.Finished :
                queryParamenter.State == TodoState.Overdue ? !t.Finished &&
                    (t.DateRange.End != DateOnly.MinValue ? t.DateRange.End < dateOnlyNow : t.DateRange.Start < dateOnlyNow) :
                queryParamenter.State == TodoState.Non_Overdue ? !t.Finished &&
                    (t.DateRange.End != DateOnly.MinValue ? t.DateRange.End >= dateOnlyNow : t.DateRange.Start >= dateOnlyNow) :
                true
            )
            .Where(t => 
                string.IsNullOrEmpty(queryParamenter.Description) ? true :
                    t.Description.ToLower().Contains(queryParamenter.Description.Trim().ToLower())
            );
    }
}
