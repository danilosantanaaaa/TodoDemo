namespace TodoApp.Domain.Todos.ValueObjects;

public static class DateRangeError
{
    public static Error NotAllowStartGreaterEnd =>
        Error.Validation("DateRange.NotAllowStartGreaterEnd", "Não é permitido a Data Inicial ser maior que a Data Final.");

    public static Error NotAllowFieldEmpty =>
        Error.Validation("DateRange.NotAllowFieldEmpty", "Os campos deve ser preenchido com o intervalo correto.");

    public static Error NotAllowLessCurrentDate =>
        Error.Validation("DateRange.NotAllowLessCurrentDate", "Não é permitido informar uma Data menor que a Data Atual.");
}