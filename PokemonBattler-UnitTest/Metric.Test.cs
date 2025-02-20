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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        BattleTeam team = new BattleTeam(pokemon);
        var winsAgainst = new List<BattleTeam>();
        var lossesAgainst = new List<BattleTeam>();
        var tiesAgainst = new List<BattleTeam>();

        // Act
        var metric = new Metric
        {
            Team = team,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(team, metric.Team);
        Assert.Equal(winsAgainst, metric.WinsAgainst);
        Assert.Equal(lossesAgainst, metric.LossesAgainst);
        Assert.Equal(tiesAgainst, metric.TiesAgainst);
    }

    [Fact]
    public void TestMetricWins()
    {
        // Arrange
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        BattleTeam team1 = new BattleTeam(pokemon);

        BattleTeam team2 = new BattleTeam(PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"))); // Pikachu
        BattleTeam team3 = new BattleTeam(PokedexRepository.CreatePokemon("" + 1, NatureRepository.GetNature("adamant"))); // Bulbasaur
        var winsAgainst = new List<BattleTeam>
        {
            team2, team3
        };
        var lossesAgainst = new List<BattleTeam>();
        var tiesAgainst = new List<BattleTeam>();

        // Act
        var metric = new Metric
        {
            Team = team1,
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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        BattleTeam team = new BattleTeam(pokemon);

        var winsAgainst = new List<BattleTeam>();

        BattleTeam team2 = new BattleTeam(PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"))); // Pikachu
        BattleTeam team3 = new BattleTeam(PokedexRepository.CreatePokemon("" + 1, NatureRepository.GetNature("adamant"))); // Bulbasaur
        var lossesAgainst = new List<BattleTeam>
        {
            team2, team3
        };
        var tiesAgainst = new List<BattleTeam>();

        // Act
        var metric = new Metric
        {
            Team = team,
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
        var pokemon = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        BattleTeam team = new BattleTeam(pokemon);

        var winsAgainst = new List<BattleTeam>();
        var lossesAgainst = new List<BattleTeam>();

        BattleTeam team2 = new BattleTeam(PokedexRepository.CreatePokemon("" + 25, NatureRepository.GetNature("adamant"))); // Pikachu
        BattleTeam team3 = new BattleTeam(PokedexRepository.CreatePokemon("" + 1, NatureRepository.GetNature("adamant"))); // Bulbasaur
        var tiesAgainst = new List<BattleTeam>
        {
            team2, team3
        };

        // Act
        var metric = new Metric
        {
            Team = team,
            WinsAgainst = winsAgainst,
            LossesAgainst = lossesAgainst,
            TiesAgainst = tiesAgainst
        };

        // Assert
        Assert.Equal(2, metric.Ties);
    }
}