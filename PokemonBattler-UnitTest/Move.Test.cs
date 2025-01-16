using Xunit;

public class MoveTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public MoveTest(TestFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public void TestMoveClone()
    {
        // Arrange
        var pp = 15;
        var originalMove = new Move("Ice Punch", TypeRepository.GetType("Ice"), MoveCategory.Physical, pp, 75, 1.0m);

        // Act
        var clonedMove = originalMove.Clone();

        // Assert
        Assert.Equal(originalMove.Name, clonedMove.Name);
        Assert.Equal(originalMove.Type, clonedMove.Type);
        Assert.Equal(originalMove.Category, clonedMove.Category);
        Assert.Equal(originalMove.PP, clonedMove.PP);
        Assert.Equal(originalMove.Power, clonedMove.Power);
        Assert.Equal(originalMove.Accuracy, clonedMove.Accuracy);

        clonedMove.MoveUsed();
        Assert.Equal(pp - 1, clonedMove.PP);
    }
}