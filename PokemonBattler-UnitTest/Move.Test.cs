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
        var originalMove = new Move(
            id: 8,
            name: "Ice Punch",
            type: TypeRepository.GetType("Ice"),
            category: MoveCategory.Physical,
            maxPP: pp,
            power: 75,
            accuracy: 1.0m,
            priority: 0,
            makesContact: true,
            affectedByProtect: true,
            metronome: false,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect>()
        );

        // Act
        var clonedMove = originalMove.Clone();

        // Assert
        Assert.Equal(originalMove.Name, clonedMove.Name);
        Assert.Equal(originalMove.Type, clonedMove.Type);
        Assert.Equal(originalMove.Category, clonedMove.Category);
        Assert.Equal(originalMove.PP, clonedMove.PP);
        Assert.Equal(originalMove.Power, clonedMove.Power);
        Assert.Equal(originalMove.Accuracy, clonedMove.Accuracy);
        Assert.Equal(originalMove.Priority, clonedMove.Priority);
        Assert.Equal(originalMove.MakesContact, clonedMove.MakesContact);
        Assert.Equal(originalMove.AffectedByProtect, clonedMove.AffectedByProtect);
        Assert.Equal(originalMove.Metronome, clonedMove.Metronome);
        Assert.Equal(originalMove.AffectedBySnatch, clonedMove.AffectedBySnatch);
        Assert.Equal(originalMove.AffectedByMirrorMove, clonedMove.AffectedByMirrorMove);

        clonedMove.MoveUsed();
        Assert.Equal(pp - 1, clonedMove.PP);
    }

    [Fact]
    public void TestIsNonDamage_True()
    {
        // Arrange
        var move = new Move(
            id: 45,
            name: "Growl",
            type: TypeRepository.GetType("Normal"),
            category: MoveCategory.Status,
            maxPP: 40,
            power: null,
            accuracy: 1.0m,
            priority: 0,
            makesContact: false,
            affectedByProtect: true,
            metronome: true,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect> { new OpponentDefenseEffect() }
        );

        // Act
        var isNonDamage = move.IsNonDamage;

        // Assert
        Assert.True(isNonDamage);
    }

    [Fact]
    public void TestIsNonDamage_False()
    {
        // Arrange
        var move = new Move(
            id: 33,
            name: "Tackle",
            type: TypeRepository.GetType("Normal"),
            category: MoveCategory.Physical,
            maxPP: 35,
            power: 40,
            accuracy: 1.0m,
            priority: 0,
            makesContact: true,
            affectedByProtect: true,
            metronome: false,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect>()
        );

        // Act
        var isNonDamage = move.IsNonDamage;

        // Assert
        Assert.False(isNonDamage);
    }
}