using Xunit;
using System.Collections.Generic;

public class MetricTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public MetricTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestMetricInitialization()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var winsAgainst = new List<Pokemon>();
        var lossesAgainst = new List<Pokemon>();
        var tiesAgainst = new List<Pokemon>();

        // Act
        var metric = new Metric
        {
            Pokemon = pokemon,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(pokemon, metric.Pokemon);
        Assert.Equal(winsAgainst, metric.WinsAgainst);
        Assert.Equal(lossesAgainst, metric.LossesAgainst);
        Assert.Equal(tiesAgainst, metric.TiesAgainst);
    }

    [Fact]
    public void TestMetricWins()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var winsAgainst = new List<Pokemon>
        {
            PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant")), // Pikachu
            PokedexRepository.CreatePokemon(1, NatureRepository.GetNature("adamant")) // Bulbasaur
        };
        var lossesAgainst = new List<Pokemon>();
        var tiesAgainst = new List<Pokemon>();

        // Act
        var metric = new Metric
        {
            Pokemon = pokemon,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(2, metric.Wins);
    }

    [Fact]
    public void TestMetricLosses()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var winsAgainst = new List<Pokemon>();
        var lossesAgainst = new List<Pokemon>
        {
            PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant")), // Pikachu
            PokedexRepository.CreatePokemon(1, NatureRepository.GetNature("adamant")) // Bulbasaur
        };
        var tiesAgainst = new List<Pokemon>();

        // Act
        var metric = new Metric
        {
            Pokemon = pokemon,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(2, metric.Losses);
    }

    [Fact]
    public void TestMetricTies()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon(129, NatureRepository.GetNature("adamant")); // Magikarp
        var winsAgainst = new List<Pokemon>();
        var lossesAgainst = new List<Pokemon>();
        var tiesAgainst = new List<Pokemon>
        {
            PokedexRepository.CreatePokemon(25, NatureRepository.GetNature("adamant")), // Pikachu
            PokedexRepository.CreatePokemon(1, NatureRepository.GetNature("adamant")) // Bulbasaur
        };

        // Act
        var metric = new Metric
        {
            Pokemon = pokemon,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(2, metric.Ties);
    }
}