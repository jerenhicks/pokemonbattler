using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;

public class MoveSetRepositoryTests
{
    [Fact]
    public void LoadMoveSetsFromFile_CorrectlyParsesMoveSets()
    {
        // Arrange
        var filePath = "test_movesets.json";

        var moveSet = new MoveSet(9, "Bulbasaur", new Dictionary<string, List<string>>
        {
            { "tackle", new List<string> { "9L10", "8M" } },
            { "growl", new List<string> { "9L11", "8M" } },
            { "hyperbeam", new List<string> { "9M" } },
            { "leechseed", new List<string> { "9E" } }
        });
        moveSet.Unpack();

        var expectedMoveSets = new Dictionary<int, MoveSet>
        {
            {
                9, moveSet
            }
        };





        // Create a temporary JSON file for testing
        var jsonData = JsonConvert.SerializeObject(expectedMoveSets.Values);
        File.WriteAllText(filePath, jsonData);

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
            Assert.Equal(expectedMoveSet.Value.LevelUpMoves["9"].Count, actualMoveSet.LevelUpMoves["9"].Count);
            Assert.Equal(expectedMoveSet.Value.MachineMoves["9"].Count, actualMoveSet.MachineMoves["9"].Count);
            Assert.Equal(expectedMoveSet.Value.EggMoves["9"].Count, actualMoveSet.EggMoves["9"].Count);
        }

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public void GetMoveSets_ReturnsCorrectMoveSets()
    {
        // Arrange
        var filePath = "test_movesets.json";
        var moveSet = new MoveSet(9, "Bulbasaur", new Dictionary<string, List<string>>
        {
            { "tackle", new List<string> { "9L10", "8M" } },
            { "growl", new List<string> { "9L11", "8M" } },
            { "hyperbeam", new List<string> { "9M" } },
            { "leechseed", new List<string> { "9E" } }
        });
        moveSet.Unpack();
        var expectedMoveSets = new Dictionary<int, MoveSet>
        {
            {
                9,moveSet
            }
        };

        // Create a temporary JSON file for testing
        var jsonData = JsonConvert.SerializeObject(expectedMoveSets.Values);
        File.WriteAllText(filePath, jsonData);

        MoveSetRepository.LoadMoveSetsFromFile(filePath);

        // Act
        var result = MoveSetRepository.GetMoveSets();

        // Assert
        Assert.Equal(expectedMoveSets.Count, result.Count);
        foreach (var expectedMoveSet in expectedMoveSets)
        {
            Assert.True(result.ContainsKey(expectedMoveSet.Key));
            var actualMoveSet = result[expectedMoveSet.Key];
            Assert.Equal(expectedMoveSet.Value.PokemonID, actualMoveSet.PokemonID);
            Assert.Equal(expectedMoveSet.Value.LevelUpMoves["9"].Count, actualMoveSet.LevelUpMoves["9"].Count);
            Assert.Equal(expectedMoveSet.Value.MachineMoves["9"].Count, actualMoveSet.MachineMoves["9"].Count);
            Assert.Equal(expectedMoveSet.Value.EggMoves["9"].Count, actualMoveSet.EggMoves["9"].Count);
        }
    }

    [Fact]
    public void BuildRandomMoveSet_ReturnsCorrectNumberOfMoves()
    {
        // Arrange
        var filePath = "test_movesets.json";
        var moveSet = new MoveSet(9, "Bulbasaur", new Dictionary<string, List<string>>
        {
            { "tackle", new List<string> { "9L10", "8M" } },
            { "growl", new List<string> { "9L11", "8M" } },
            { "hyperbeam", new List<string> { "9M" } },
            { "leechseed", new List<string> { "9E" } }
        });
        moveSet.Unpack();

        var moveSets = new List<MoveSet> { moveSet };
        var jsonData = JsonConvert.SerializeObject(moveSets);
        File.WriteAllText(filePath, jsonData);

        MoveSetRepository.LoadMoveSetsFromFile(filePath);

        // Act
        var result = MoveSetRepository.BuildRandomMoveSet(9, 9, 4, true, true, true, true, true, true, true);

        // Assert
        Assert.Equal(4, result.Count);

        // Clean up
        File.Delete(filePath);
    }
}