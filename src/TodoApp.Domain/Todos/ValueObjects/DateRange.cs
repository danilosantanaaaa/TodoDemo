
namespace TodoApp.Domain.Todos.ValueObjects;

public record DateRange
{
    public DateOnly Start { get; }
    public DateOnly End { get; }

    private DateRange(DateOnly start, DateOnly end)
    {
        Start = start == DateOnly.MinValue ? DateOnly.FromDateTime(DateTime.Now) : start;
        End = end;
    }

    private DateRange() { }

    public static Result<DateRange> Create(DateOnly start, DateOnly end, DateTime dateTimeProvider)
    {
        if (IsStartGreaterEnd(start, end))
        {
            return DateRangeError.NotAllowStartGreaterEnd;
        }

        if (IsRequiredStart(start, end))
        {
            return DateRangeError.NotAllowFieldEmpty;
        }

        if (IsLessCurrentDate(start, end, dateTimeProvider))
        {
            return DateRangeError.NotAllowLessCurrentDate;
        }

        return new DateRange(start, end);
    }

    public static DateRange CreateEmpty()
    {

        return new DateRange();
    }

    private static bool IsStartGreaterEnd(DateOnly start, DateOnly end)
    {
        return start > end && end != DateOnly.MinValue;
    }

    private static bool IsRequiredStart(DateOnly start, DateOnly end)
    {
        return start == DateOnly.MinValue && end != DateOnly.MinValue;
    }

    private static bool IsLessCurrentDate(DateOnly start, DateOnly end, DateTime dateTimeProvider)
    {
        var dateOnlyProvider = DateOnly.FromDateTime(dateTimeProvider);

        return start != DateOnly.MinValue && end == DateOnly.MinValue && start < dateOnlyProvider ||
            start != DateOnly.MinValue && end != DateOnly.MinValue && end < dateOnlyProvider;
    }
}