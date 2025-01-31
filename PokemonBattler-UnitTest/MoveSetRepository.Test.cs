using System;
using System.Collections.Generic;
using System.IO;
using Xunit;


public class MoveSetRepositoryTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public MoveSetRepositoryTests(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void LoadMoveSetsFromFile_CorrectlyParsesMoveSets()
    {
        // Arrange
        var filePath = "test_movesets.csv";

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

        var expectedMoveSets = new Dictionary<int, MoveSet>
        {
            {
                1, new MoveSet
                {
                    PokemonID = 1,
                    LevelUpMoves = new List<Move> { tackle, growl },
                    MachineMoves = new List<Move> { hyperbeam },
                    EggMoves = new List<Move> { earthquake, tailWhip }
                }
            }
        };

        // Create a temporary CSV file for testing
        File.WriteAllLines(filePath, new[]
        {
            "1,33/45,36,92/174"
        });

        // Act
        MoveSetRepository.LoadMoveSetsFromFile(filePath);
        var result = MoveSetRepository.GetMoveSets();

        // Assert
        Assert.Equal(expectedMoveSets.Count, result.Count);
        foreach (var expectedMoveSet in expectedMoveSets)
        {
            Assert.True(result.ContainsKey(expectedMoveSet.Key));
            var actualMoveSet = result[expectedMoveSet.Key];
            Assert.Equal(expectedMoveSet.Value.PokemonID, actualMoveSet.PokemonID);
            Assert.Equal(expectedMoveSet.Value.LevelUpMoves.Count, actualMoveSet.LevelUpMoves.Count);
            Assert.Equal(expectedMoveSet.Value.MachineMoves.Count, actualMoveSet.MachineMoves.Count);
            Assert.Equal(expectedMoveSet.Value.EggMoves.Count, actualMoveSet.EggMoves.Count);
        }

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public void LoadMoveSetsFromFile_EmptyFile_ReturnsEmptyDictionary()
    {
        // Arrange
        var filePath = "empty_movesets.csv";

        // Create an empty CSV file for testing
        File.WriteAllText(filePath, string.Empty);

        // Act
        MoveSetRepository.LoadMoveSetsFromFile(filePath);
        var result = MoveSetRepository.GetMoveSets();

        // Assert
        Assert.Empty(result);

        // Clean up
        File.Delete(filePath);
    }
}