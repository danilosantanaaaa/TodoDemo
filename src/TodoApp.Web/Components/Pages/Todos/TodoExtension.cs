using System.Globalization;

using TodoApp.Application.Common.Interfaces;

namespace TodoApp.Web.Components.Pages.Todos;

public static class TodoExtension
{
    public static string FormatDate(
        this DateOnly dateStart,
        DateOnly dateEnd,
        IDateTimeProvider dateTimeProvider)
    {
        return FormatDate(
            dateStart.ToDateTime(TimeOnly.MinValue),
            dateEnd.ToDateTime(TimeOnly.MinValue),
            dateTimeProvider);
    }

    public static string FormatDate(
        this DateTime? dateStart,
        DateTime? dateEnd,
        IDateTimeProvider dateTimeProvider)
    {

        if (dateStart is not null && dateEnd is not null && dateStart.Value.Date != dateEnd.Value.Date && dateEnd.Value != DateTime.MinValue)
        {
            return $"{dateStart.FormatDate(dateTimeProvider)} - {dateEnd.FormatDate(dateTimeProvider)}";
        }

        return dateStart.FormatDate(dateTimeProvider);
    }

    public static string FormatDate(this DateTime? date, IDateTimeProvider dateTimeProvider)
    {
        if (date is null) return string.Empty;

        if (date.Value.Date == dateTimeProvider.UtcNow.Date)
        {
            return "Hoje";
        }
        else if (date.Value.Date == dateTimeProvider.UtcNow.AddDays(1).Date)
        {
            return "Amanhã";
        }
        else
        {
            return date.Value.ToString(
                "dddd d MMMM yyyy",
                CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }

    public static bool ExpiredTodo(
        this DateTime dateStart,
        DateTime dateEnd,
        IDateTimeProvider dateTimeProvider)
    {
        return ExpiredTodo(
            DateOnly.FromDateTime(dateStart.Date),
            DateOnly.FromDateTime(dateEnd.Date),
            dateTimeProvider);
    }

    public static bool ExpiredTodo(
        this DateOnly dateStart,
        DateOnly dateEnd,
        IDateTimeProvider dateTimeProvider)
    {
        var dateCurrent = DateOnly.FromDateTime(dateTimeProvider.UtcNow.Date);

        if (dateStart.ExistIntervalBeetweenThisDateAnd(dateEnd))
        {
            return dateCurrent > dateStart && dateCurrent > dateEnd;
        }

        return dateCurrent > dateStart;
    }

    public static bool ExistIntervalBeetweenThisDateAnd(this DateOnly dateStart, DateOnly dateEnd)
    {
        return dateStart != DateOnly.MinValue &&
            dateEnd != DateOnly.MinValue;
    }
}