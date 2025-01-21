using Xunit;

public class RangeTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public RangeTest(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void TestRange_AllTrue()
    {
        // Arrange
        var range = new Range(true, true, true, true, true, true);

        // Assert
        Assert.True(range.Opponent1);
        Assert.True(range.Opponent2);
        Assert.True(range.Opponent3);
        Assert.True(range.Self);
        Assert.True(range.Ally1);
        Assert.True(range.Ally2);
    }

    [Fact]
    public void TestRange_AllFalse()
    {
        // Arrange
        var range = new Range(false, false, false, false, false, false);

        // Assert
        Assert.False(range.Opponent1);
        Assert.False(range.Opponent2);
        Assert.False(range.Opponent3);
        Assert.False(range.Self);
        Assert.False(range.Ally1);
        Assert.False(range.Ally2);
    }

    [Fact]
    public void TestRange_MixedValues()
    {
        // Arrange
        var range = new Range(true, false, true, false, true, false);

        // Assert
        Assert.True(range.Opponent1);
        Assert.False(range.Opponent2);
        Assert.True(range.Opponent3);
        Assert.False(range.Self);
        Assert.True(range.Ally1);
        Assert.False(range.Ally2);
    }
}