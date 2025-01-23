namespace TodoApp.Domain.UnitTests.Todos.ValueObjects;

public class DateRangeTests
{
    public static DateTime _dateTimeProviderMock = DateTime.UtcNow;


    [Fact]
    public void Create_ShouldReturnError_WhenStartGreaterEnd()
    {
        // Arrange
        var start = DateOnly.FromDateTime(_dateTimeProviderMock.AddDays(1));
        var end = DateOnly.FromDateTime(_dateTimeProviderMock);

        // Act
        var result = DateRange.Create(start, end, _dateTimeProviderMock);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(DateRangeError.NotAllowStartGreaterEnd);
    }

    [Fact]
    public void Create_ShouldReturnError_WhenStartIsEmpty()
    {
        // Arrange
        var start = DateOnly.MinValue;
        var end = DateOnly.FromDateTime(_dateTimeProviderMock);

        // Act
        var result = DateRange.Create(start, end, _dateTimeProviderMock);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(DateRangeError.NotAllowFieldEmpty);

    }

    [Theory]
    [MemberData(nameof(GetStartOrLessLessCurrentDate), MemberType = typeof(DateRangeTests))]
    public void Create_ShouldReturnErrors_WhenStartOrEndLessCurrentDate(
        DateOnly start,
        DateOnly end)
    {
        // Arrange && Act
        var result = DateRange.Create(start, end, _dateTimeProviderMock.AddDays(1));

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(DateRangeError.NotAllowLessCurrentDate);
    }

    public static TheoryData<DateOnly, DateOnly> GetStartOrLessLessCurrentDate()
    {
        return new TheoryData<DateOnly, DateOnly>
        {
            {DateOnly.FromDateTime(_dateTimeProviderMock.AddDays(-1)), DateOnly.MinValue},
            {DateOnly.FromDateTime(_dateTimeProviderMock.AddDays(-2)), DateOnly.FromDateTime(_dateTimeProviderMock.AddDays(-1))}
        };
    }
}