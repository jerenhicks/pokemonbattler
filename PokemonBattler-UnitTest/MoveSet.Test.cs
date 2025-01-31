using System.Collections.Generic;
using Xunit;


public class MoveSetTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public MoveSetTests(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void MoveSet_Initialization_SetsPropertiesCorrectly()
    {
        // Arrange
        int pokemonID = 1;

        var growl = new Move(
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
           effects: new List<BaseEffect> { new GrowlEffect() }
       );
        var tackle = new Move(
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

        var tailWhip = new Move(
            id: 36,
            name: "Tail Whip",
            type: TypeRepository.GetType("Normal"),
            category: MoveCategory.Status,
            maxPP: 30,
            power: null,
            accuracy: 1.0m,
            priority: 0,
            makesContact: false,
            affectedByProtect: true,
            metronome: false,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect> { }
        );

        var hyperbeam = new Move(
            id: 92,
            name: "Hyper Beam",
            type: TypeRepository.GetType("Normal"),
            category: MoveCategory.Special,
            maxPP: 5,
            power: 150,
            accuracy: 0.9m,
            priority: 0,
            makesContact: false,
            affectedByProtect: true,
            metronome: false,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect> { }
        );

        var earthquake = new Move(
            id: 174,
            name: "Earthquake",
            type: TypeRepository.GetType("Ground"),
            category: MoveCategory.Physical,
            maxPP: 10,
            power: 100,
            accuracy: 1.0m,
            priority: 0,
            makesContact: false,
            affectedByProtect: true,
            metronome: false,
            affectedBySnatch: false,
            affectedByMirrorMove: true,
            range: Range.Normal,
            effects: new List<BaseEffect> { }
        );

        var levelUpMoves = new List<Move> { tackle, growl };
        var eggMoves = new List<Move> { tailWhip };
        var machineMoves = new List<Move> { hyperbeam, earthquake };

        // Act
        var moveSet = new MoveSet
        {
            PokemonID = pokemonID,
            LevelUpMoves = levelUpMoves,
            EggMoves = eggMoves,
            MachineMoves = machineMoves
        };

        // Assert
        Assert.Equal(pokemonID, moveSet.PokemonID);
        Assert.Equal(levelUpMoves, moveSet.LevelUpMoves);
        Assert.Equal(eggMoves, moveSet.EggMoves);
        Assert.Equal(machineMoves, moveSet.MachineMoves);
    }
}