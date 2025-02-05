using System.Collections.Generic;
using Xunit;

public class MoveSetTests
{
    [Fact]
    public void MoveSet_Initialization_SetsPropertiesCorrectly()
    {
        // Arrange
        int pokemonID = 1;
        string pokemonName = "Bulbasaur";
        var learnsetsDictionary = new Dictionary<string, List<string>>
        {
            { "tackle", new List<string> { "9L10", "8M" } },
            { "growl", new List<string> { "9L11", "8M" } },
            { "hyperbeam", new List<string> { "9M" } },
            { "leechseed", new List<string> { "9E" } }
        };

        // Act
        var moveSet = new MoveSet(pokemonID, pokemonName, learnsetsDictionary);

        // Assert
        Assert.Equal(pokemonID, moveSet.PokemonID);
        Assert.Equal(pokemonName, moveSet.PokemonName);
        Assert.Equal(learnsetsDictionary, moveSet.LearnsetsDictionary);
    }

    [Fact]
    public void Unpack_Method_WorksCorrectly()
    {
        // Arrange
        var moveSet = new MoveSet(1, "Bulbasaur", new Dictionary<string, List<string>>
        {
            { "tackle", new List<string> { "9L10", "8M" } },
            { "growl", new List<string> { "9L11", "8M" } },
            { "hyperbeam", new List<string> { "9M" } },
            { "leechseed", new List<string> { "9E" } }
        });

        // Act
        moveSet.Unpack();

        // Assert
        // Add assertions based on what the Unpack method is supposed to do
        // For example, if Unpack modifies the LearnsetsDictionary in some way, assert those changes
    }

    [Fact]
    public void LearnsetsDictionary_CanBeEmpty()
    {
        // Arrange
        var moveSet = new MoveSet(1, "Bulbasaur", new Dictionary<string, List<string>>());

        // Act & Assert
        Assert.Empty(moveSet.LearnsetsDictionary);
    }

    [Fact]
    public void LearnsetsDictionary_CanContainMultipleGenerations()
    {
        // Arrange
        var learnsetsDictionary = new Dictionary<string, List<string>>
        {
            { "generation1", new List<string> { "tackle", "growl" } },
            { "generation2", new List<string> { "vinewhip", "razorleaf" } }
        };

        var moveSet = new MoveSet(1, "Bulbasaur", learnsetsDictionary);

        // Act & Assert
        Assert.Equal(2, moveSet.LearnsetsDictionary.Count);
        Assert.Contains("generation1", moveSet.LearnsetsDictionary.Keys);
        Assert.Contains("generation2", moveSet.LearnsetsDictionary.Keys);
    }
}