using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
        var filePath = "test_movesets.json";


        Dictionary<string, List<MoveAbbreviated>> LevelUpMoves = new Dictionary<string, List<MoveAbbreviated>>();
        LevelUpMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> EggMoves = new Dictionary<string, List<MoveAbbreviated>>();
        EggMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(73, "Leech Seed"), new MoveAbbreviated(33, "Hyper Beam") });
        Dictionary<string, List<MoveAbbreviated>> MachineMoves = new Dictionary<string, List<MoveAbbreviated>>();
        MachineMoves.Add("8", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> TutorMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> RestrictedMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> DreamWorldMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> EventMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> VirtualConsoleMoves = new Dictionary<string, List<MoveAbbreviated>>();


        var moveSet = new MoveSet("Bulbasaur", "1", LevelUpMoves, EggMoves, MachineMoves, TutorMoves, RestrictedMoves, DreamWorldMoves, EventMoves, VirtualConsoleMoves);
        moveSet.Unpack();

        var expectedMoveSets = new Dictionary<string, MoveSet>
        {
            {
                "1", moveSet
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
            Assert.Equal(expectedMoveSet.Value.MachineMoves["8"].Count, actualMoveSet.MachineMoves["8"].Count);
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
        Dictionary<string, List<MoveAbbreviated>> LevelUpMoves = new Dictionary<string, List<MoveAbbreviated>>();
        LevelUpMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> EggMoves = new Dictionary<string, List<MoveAbbreviated>>();
        EggMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(73, "Leech Seed"), new MoveAbbreviated(33, "Hyper Beam") });
        Dictionary<string, List<MoveAbbreviated>> MachineMoves = new Dictionary<string, List<MoveAbbreviated>>();
        MachineMoves.Add("8", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> TutorMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> RestrictedMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> DreamWorldMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> EventMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> VirtualConsoleMoves = new Dictionary<string, List<MoveAbbreviated>>();


        var moveSet = new MoveSet("Bulbasaur", "1", LevelUpMoves, EggMoves, MachineMoves, TutorMoves, RestrictedMoves, DreamWorldMoves, EventMoves, VirtualConsoleMoves);
        moveSet.Unpack();
        var expectedMoveSets = new Dictionary<string, MoveSet>
        {
            {
                "1",moveSet
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
            Assert.Equal(expectedMoveSet.Value.MachineMoves["8"].Count, actualMoveSet.MachineMoves["8"].Count);
            Assert.Equal(expectedMoveSet.Value.EggMoves["9"].Count, actualMoveSet.EggMoves["9"].Count);
        }
    }

    [Fact]
    public void BuildRandomMoveSet_ReturnsCorrectNumberOfMoves()
    {
        // Arrange
        var filePath = "test_movesets.json";
        Dictionary<string, List<MoveAbbreviated>> LevelUpMoves = new Dictionary<string, List<MoveAbbreviated>>();
        LevelUpMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> EggMoves = new Dictionary<string, List<MoveAbbreviated>>();
        EggMoves.Add("9", new List<MoveAbbreviated> { new MoveAbbreviated(73, "Leech Seed"), new MoveAbbreviated(33, "Hyper Beam") });
        Dictionary<string, List<MoveAbbreviated>> MachineMoves = new Dictionary<string, List<MoveAbbreviated>>();
        MachineMoves.Add("8", new List<MoveAbbreviated> { new MoveAbbreviated(33, "Tackle"), new MoveAbbreviated(45, "Growl") });
        Dictionary<string, List<MoveAbbreviated>> TutorMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> RestrictedMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> DreamWorldMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> EventMoves = new Dictionary<string, List<MoveAbbreviated>>();
        Dictionary<string, List<MoveAbbreviated>> VirtualConsoleMoves = new Dictionary<string, List<MoveAbbreviated>>();

        var moveSet = new MoveSet("Bulbasaur", "1", LevelUpMoves, EggMoves, MachineMoves, TutorMoves, RestrictedMoves, DreamWorldMoves, EventMoves, VirtualConsoleMoves);
        moveSet.Unpack();

        var moveSets = new List<MoveSet> { moveSet };
        var jsonData = JsonConvert.SerializeObject(moveSets);
        File.WriteAllText(filePath, jsonData);

        MoveSetRepository.LoadMoveSetsFromFile(filePath);

        // Act
        var result = MoveSetRepository.BuildRandomMoveSet("1", Generation.NINE, 4, true, true, true, true, true, true, true);

        // Assert
        Assert.Equal(4, result.Count);

        // Clean up
        File.Delete(filePath);
    }
}