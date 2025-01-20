
public class StatModifiersTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public StatModifiersTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestChangeAtkStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeAtkStage(3);

        // Assert
        Assert.Equal(3, statModifiers.AtkStage);

        // Act
        statModifiers.ChangeAtkStage(4);

        // Assert
        Assert.Equal(6, statModifiers.AtkStage); // Should be capped at 6

        // Act
        statModifiers.ChangeAtkStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.AtkStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeDefStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeDefStage(3);

        // Assert
        Assert.Equal(3, statModifiers.DefStage);

        // Act
        statModifiers.ChangeDefStage(4);

        // Assert
        Assert.Equal(6, statModifiers.DefStage); // Should be capped at 6

        // Act
        statModifiers.ChangeDefStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.DefStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeSpAtkStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeSpAtkStage(3);

        // Assert
        Assert.Equal(3, statModifiers.SpAtkStage);

        // Act
        statModifiers.ChangeSpAtkStage(4);

        // Assert
        Assert.Equal(6, statModifiers.SpAtkStage); // Should be capped at 6

        // Act
        statModifiers.ChangeSpAtkStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.SpAtkStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeSpDefStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeSpDefStage(3);

        // Assert
        Assert.Equal(3, statModifiers.SpDefStage);

        // Act
        statModifiers.ChangeSpDefStage(4);

        // Assert
        Assert.Equal(6, statModifiers.SpDefStage); // Should be capped at 6

        // Act
        statModifiers.ChangeSpDefStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.SpDefStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeSpeedStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeSpeedStage(3);

        // Assert
        Assert.Equal(3, statModifiers.SpeedStage);

        // Act
        statModifiers.ChangeSpeedStage(4);

        // Assert
        Assert.Equal(6, statModifiers.SpeedStage); // Should be capped at 6

        // Act
        statModifiers.ChangeSpeedStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.SpeedStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeAccuracyStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeAccuracyStage(3);

        // Assert
        Assert.Equal(3, statModifiers.AccuracyStage);

        // Act
        statModifiers.ChangeAccuracyStage(4);

        // Assert
        Assert.Equal(6, statModifiers.AccuracyStage); // Should be capped at 6

        // Act
        statModifiers.ChangeAccuracyStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.AccuracyStage); // Should be capped at -6
    }

    [Fact]
    public void TestChangeEvasionStage()
    {
        // Arrange
        var statModifiers = new StatModifiers();

        // Act
        statModifiers.ChangeEvasionStage(3);

        // Assert
        Assert.Equal(3, statModifiers.EvasionStage);

        // Act
        statModifiers.ChangeEvasionStage(4);

        // Assert
        Assert.Equal(6, statModifiers.EvasionStage); // Should be capped at 6

        // Act
        statModifiers.ChangeEvasionStage(-12);

        // Assert
        Assert.Equal(-6, statModifiers.EvasionStage); // Should be capped at -6
    }

    [Fact]
    public void TestGetAtkModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeAtkStage(3);

        // Act
        var result = statModifiers.GetAtkModifier();

        // Assert
        Assert.Equal((5.0 / 2.0), result);
    }

    [Fact]
    public void TestGetDefModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeDefStage(3);

        // Act
        var result = statModifiers.GetDefModifier();

        // Assert
        Assert.Equal((5.0 / 2.0), result);
    }

    [Fact]
    public void TestGetSpAtkModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeSpAtkStage(3);

        // Act
        var result = statModifiers.GetSpAtkModifier();

        // Assert
        Assert.Equal((5.0 / 2.0), result);
    }

    [Fact]
    public void TestGetSpDefModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeSpDefStage(3);

        // Act
        var result = statModifiers.GetSpDefModifier();

        // Assert
        Assert.Equal((5.0 / 2.0), result);
    }

    [Fact]
    public void TestGetSpeedModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeSpeedStage(3);

        // Act
        var result = statModifiers.GetSpeedModifier();

        // Assert
        Assert.Equal((5.0 / 2.0), result);
    }

    [Fact]
    public void TestGetAccuracyModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeAccuracyStage(3);

        // Act
        var result = statModifiers.GetAccuracyModifier();

        // Assert
        Assert.Equal((6.0 / 3.0), result);
    }

    [Fact]
    public void TestGetEvasionModifier()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeEvasionStage(3);

        // Act
        var result = statModifiers.GetEvasionModifier();

        // Assert
        Assert.Equal((6.0 / 3.0), result);
    }

    [Fact]
    public void TestResetAll()
    {
        // Arrange
        var statModifiers = new StatModifiers();
        statModifiers.ChangeAtkStage(3);
        statModifiers.ChangeDefStage(3);
        statModifiers.ChangeSpAtkStage(3);
        statModifiers.ChangeSpDefStage(3);
        statModifiers.ChangeSpeedStage(3);
        statModifiers.ChangeAccuracyStage(3);
        statModifiers.ChangeEvasionStage(3);

        // Act
        statModifiers.ResetAll();

        // Assert
        Assert.Equal(0, statModifiers.AtkStage);
        Assert.Equal(0, statModifiers.DefStage);
        Assert.Equal(0, statModifiers.SpAtkStage);
        Assert.Equal(0, statModifiers.SpDefStage);
        Assert.Equal(0, statModifiers.SpeedStage);
        Assert.Equal(0, statModifiers.AccuracyStage);
        Assert.Equal(0, statModifiers.EvasionStage);
    }
}