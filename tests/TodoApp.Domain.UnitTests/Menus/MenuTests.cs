namespace TodoApp.Domain.UnitTests.Menus;

public class MenuTests
{
    [Fact]
    public void Create_ShouldReturnNewInstanceOfTheMenu_WhenCallMethodFactory()
    {
        // Arrange & Act
        var menu = Menu.Create(
            Constaint.Menu.Name,
            Constaint.Menu.Icon);

        // Assert
        menu.IsError.Should().BeFalse();
        menu.Value.Name.Should().Be(Constaint.Menu.Name);
        menu.Value.Icon.Should().Be(Constaint.Menu.Icon);
        menu.Value.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public void Update_ShouldModifyProperties()
    {
        // Arrange
        var menu = MenuFactory.CreateMenu();

        // Act
        menu.Update("NameUpdate", "IconUpdate");

        // Assert
        menu.Name.Should().Be("NameUpdate");
        menu.Icon.Should().Be("IconUpdate");
        menu.IsDeleted.Should().BeFalse();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void Update_ShouldNotModifyProperty_WhenValueIsEmpty(string? name, string? icon)
    {
        // Arrange
        var menu = MenuFactory.CreateMenu();

        // Act
        var result = menu.Update(name!, icon!);

        // Assert
        result.IsError.Should().BeFalse();
        menu.Name.Should().Be(Constaint.Menu.Name);
        menu.Icon.Should().Be(Constaint.Menu.Icon);
        menu.IsDeleted.Should().BeFalse();
    }
}