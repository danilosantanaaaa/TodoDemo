namespace TodoApp.Domain.UnitTests.Todos;

public class TodoTests
{
    private readonly DateTime _dateTimeProvider = new DateTime(2024, 5, 11, 12, 0, 0);


    [Fact]
    public void Create_ShouldCreateInstanceOfTheTodo()
    {
        // Arrange & Act
        var todoResult = Todo.Create(
            Constaint.Todo.Description,
            Constaint.Todo.UserId,
            Constaint.Menu.Id,
            Constaint.Todo.TimeRemember);

        var todo = todoResult.Value;

        // Assert
        todoResult.IsError.Should().BeFalse();
        todo.Description.Should().Be(Constaint.Todo.Description);
        todo.UserId.Should().Be(Constaint.Todo.UserId);
        todo.MenuId.Should().Be(Constaint.Menu.Id);
        todo.TimeRemember.Should().Be(Constaint.Todo.TimeRemember);
        todo.Finished.Should().BeFalse();
        todo.DateRange.Should().NotBeNull();
        todo.Id.Should().NotBeEmpty();
        todo.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public void Update_ShouldNotAllow_WhenFinishedTodo()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();
        todo.MarkAsFinish(_dateTimeProvider);
        string new_description = "Updated";
        TimeOnly new_remember = new TimeOnly(13, 0);

        // Act
        var result = todo.Update(new_description, new_remember);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should()
            .ContainEquivalentOf(TodoError.NotAllowWhenFinished);
    }

    [Fact]
    public void Update_ShouldAllow_WhenNotFinishedTodo()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();
        string new_description = "Updated";
        TimeOnly new_remember = new TimeOnly(13, 0);

        // Act
        var result = todo.Update(new_description, new_remember);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(ResultState.Updated);
        todo.Description.Should().Be(new_description);
        todo.TimeRemember.Should().Be(new_remember);
    }

    [Fact]
    public void MarkAsPendent_ShouldAssignFalse()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();

        // Act
        todo.MarkAsPendent();

        // Assert
        todo.Finished.Should().BeFalse();
    }

    [Fact]
    public void MarkAsFinish()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();

        // Act
        todo.MarkAsFinish(_dateTimeProvider);

        // Assert
        todo.Finished.Should().BeTrue();
    }

    [Fact]
    public void AddDates_ShouldReturnErrors_WhenFinishedTodo()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();
        todo.MarkAsFinish(_dateTimeProvider);
        var dateRange = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), default, DateTime.Now).Value;

        // Act
        var result = todo.AddDates(dateRange);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(TodoError.NotAllowWhenFinished);
    }

    [Fact]
    public void AddDates_ShouldReturSuccess()
    {
        // Arrange
        var todo = TodoFactory.CreateTodo();
        var dateRange = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), default, DateTime.Now).Value;

        // Act
        var result = todo.AddDates(dateRange);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(ResultState.Success);
    }
}